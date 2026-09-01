using FP.Application.Contracts.Repositories;
using FP.Domain.Common;
using FP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FP.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : SoftDeletableEntity
{
    private readonly AppDbContext context;

    public Repository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await context.Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>()
            .ToListAsync();
    }

    public async Task<List<TEntity>> GetDeletedAsync()
    {
        return await context.Set<TEntity>()
            .IgnoreQueryFilters()
            .Where(x => x.IsDeleted)
            .ToListAsync();
    }

    public async Task<List<TEntity>> WhereAsync(
        Expression<Func<TEntity, bool>> predicate)
    {
        return await context.Set<TEntity>()
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate)
    {
        return await context.Set<TEntity>()
            .FirstOrDefaultAsync(predicate);
    }

    public async Task AddAsync(TEntity entity)
    {
        await context.Set<TEntity>()
            .AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        context.Set<TEntity>()
            .Update(entity);
    }

    public void Delete(TEntity entity)
    {
        // Audit information is handled centrally in AppDbContext.
        entity.IsDeleted = true;

        context.Set<TEntity>()
            .Update(entity);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<TEntity?> GetDeletedByIdAsync(int id)
    {
        return await context.Set<TEntity>()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted);
    }
}