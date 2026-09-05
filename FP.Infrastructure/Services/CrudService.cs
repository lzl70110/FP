 
using FP.Application.Common;
using FP.Application.Contracts.Identity;
using FP.Application.Contracts.Repositories;
using FP.Application.Contracts.Services;
using FP.Domain.Common;

namespace FP.Infrastructure.Services;

public class CrudService<TEntity> : ICrudService<TEntity>
    where TEntity : SoftDeletableEntity, new()
{
    private readonly IRepository<TEntity> repository;
    private readonly IEntityIdentity<TEntity>? identity;

    private static readonly HashSet<string> ProtectedProperties =
    [
        nameof(AuditableEntity.CreatedAt),
        nameof(AuditableEntity.CreatedById),
        nameof(AuditableEntity.UpdatedAt),
        nameof(AuditableEntity.UpdatedById),
        nameof(SoftDeletableEntity.IsDeleted),
        nameof(SoftDeletableEntity.DeletedAt),
        nameof(SoftDeletableEntity.DeletedById)
    ];

    public CrudService(
        IRepository<TEntity> repository,
        IEnumerable<IEntityIdentity<TEntity>> identities)
    {
        this.repository = repository;
        identity = identities.SingleOrDefault();
    }

    public async Task<TEntity?> ExecuteAsync(
        CrudCommand command,
        int? id = null,
        params CrudProperty[] properties)
    {
        return command switch
        {
            CrudCommand.Create => await CreateAsync(properties),

            CrudCommand.Read => id.HasValue
                ? await repository.GetByIdAsync(id.Value)
                : null,

            CrudCommand.Update => id.HasValue
                ? await UpdateAsync(id.Value, properties)
                : null,

            CrudCommand.Delete => id.HasValue
                ? await DeleteAsync(id.Value)
                : null,

            CrudCommand.Undelete => id.HasValue
                ? await UndeleteAsync(id.Value)
                : null,

            _ => throw new ArgumentOutOfRangeException(
                nameof(command),
                command,
                "Невалидна CRUD операция.")
        };
    }

    private async Task<TEntity> CreateAsync(
    CrudProperty[] properties)
    {
        var entity = new TEntity();

        ApplyProperties(entity, properties);

        if (identity != null)
        {
            var predicate = identity.BuildMatchPredicate(entity);

            var deletedEntity =
                await repository.FirstDeletedOrDefaultAsync(predicate);

            if (deletedEntity != null)
            {
                ApplyProperties(deletedEntity, properties);

                deletedEntity.IsDeleted = false;
                deletedEntity.DeletedAt = null;
                deletedEntity.DeletedById = null;

                repository.Update(deletedEntity);
                await repository.SaveChangesAsync();

                return deletedEntity;
            }
        }

        await repository.AddAsync(entity);
        await repository.SaveChangesAsync();

        return entity;
    }

    private async Task<TEntity?> UpdateAsync(
        int id,
        CrudProperty[] properties)
    {
        var entity = await repository.GetByIdAsync(id);

        if (entity == null)
        {
            return null;
        }

        ApplyProperties(entity, properties);

        repository.Update(entity);
        await repository.SaveChangesAsync();

        return entity;
    }

    private async Task<TEntity?> DeleteAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);

        if (entity == null)
        {
            return null;
        }

        repository.Delete(entity);
        await repository.SaveChangesAsync();

        return entity;
    }

    private async Task<TEntity?> UndeleteAsync(int id)
    {
        var entity = await repository.GetDeletedByIdAsync(id);

        if (entity == null)
        {
            return null;
        }

        entity.IsDeleted = false;
        entity.DeletedAt = null;
        entity.DeletedById = null;

        repository.Update(entity);
        await repository.SaveChangesAsync();

        return entity;
    }

    private static void ApplyProperties(
        TEntity entity,
        CrudProperty[] properties)
    {
        foreach (var property in properties)
        {
            if (ProtectedProperties.Contains(property.Name))
            {
                throw new InvalidOperationException(
                    $"Свойството '{property.Name}' е системно и не може да бъде променяно чрез CRUD операция.");
            }

            var propertyInfo = typeof(TEntity).GetProperty(property.Name);

            if (propertyInfo == null)
            {
                throw new InvalidOperationException(
                    $"Свойството '{property.Name}' не е намерено в '{typeof(TEntity).Name}'.");
            }

            if (!propertyInfo.CanWrite)
            {
                throw new InvalidOperationException(
                    $"Свойството '{property.Name}' е само за четене.");
            }

            if (property.Value != null &&
                !propertyInfo.PropertyType.IsAssignableFrom(property.Value.GetType()))
            {
                throw new InvalidOperationException(
                    $"Стойността за свойството '{property.Name}' е от несъвместим тип.");
            }

            propertyInfo.SetValue(entity, property.Value);
        }
    }
}
 
