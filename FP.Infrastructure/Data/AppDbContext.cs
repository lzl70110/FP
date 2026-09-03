using FP.Domain.Common;
using FP.Domain.Entities.Audit;
using FP.Domain.Entities.Checkers;
using FP.Domain.Entities.Departments;
using FP.Domain.Entities.Employees;
using FP.Domain.Entities.Extinguishers;
using FP.Domain.Entities.Positions;
using FP.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FP.Infrastructure.Data;

public class AppDbContext : DbContext
{
    // Temporary system user until Identity is introduced.
    private const string SystemUser = "System";

    private static readonly HashSet<string> AuditExcludedProperties =
    [
        nameof(BaseEntity.Id),
        nameof(AuditableEntity.CreatedAt),
        nameof(AuditableEntity.CreatedById),
        nameof(AuditableEntity.UpdatedAt),
        nameof(AuditableEntity.UpdatedById),
        nameof(SoftDeletableEntity.DeletedAt),
        nameof(SoftDeletableEntity.DeletedById)
    ];

    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<Checker> Checkers { get; set; } = null!;
    public DbSet<Extinguisher> Extinguishers { get; set; } = null!;
    public DbSet<ExtinguisherType> ExtinguisherTypes { get; set; } = null!;
    public DbSet<AuditEntry> AuditEntries { get; set; } = null!;

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var trackedEntries = ChangeTracker
            .Entries<SoftDeletableEntity>()
            .Where(e => e.State is
                EntityState.Added or
                EntityState.Modified)
            .ToList();

        var auditData = PrepareAuditData(trackedEntries);

        await using var transaction =
            Database.CurrentTransaction == null
                ? await Database.BeginTransactionAsync(cancellationToken)
                : null;

        try
        {
            // Save business entities first so generated IDs become available.
            var result = await base.SaveChangesAsync(cancellationToken);

            // Update entity IDs after database-generated keys are available.
            foreach (var data in auditData)
            {
                data.EntityId = data.Entity.Id;
            }

            // Create audit entries after IDs have been generated.
            foreach (var data in auditData)
            {
                AuditEntries.Add(new AuditEntry
                {
                    Timestamp = DateTime.UtcNow,
                    UserId = SystemUser,
                    EntityType = data.EntityType,
                    EntityId = data.EntityId,
                    Action = data.Action,
                    OldValues = data.OldValues,
                    NewValues = data.NewValues
                });
            }

            // Save audit records in the same transaction.
            if (auditData.Count > 0)
            {
                await base.SaveChangesAsync(cancellationToken);
            }

            if (transaction != null)
            {
                await transaction.CommitAsync(cancellationToken);
            }

            return result;
        }
        catch
        {
            if (transaction != null)
            {
                await transaction.RollbackAsync(cancellationToken);
            }

            throw;
        }
    }

    private static List<AuditData> PrepareAuditData(
        List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<SoftDeletableEntity>> entries)
    {
        var result = new List<AuditData>();

        foreach (var entry in entries)
        {
            var action = GetAuditAction(entry);

            if (action == null)
                continue;

            result.Add(new AuditData
            {
                Entity = entry.Entity,
                EntityType = entry.Metadata.ClrType.Name,
                EntityId = entry.Entity.Id,
                Action = action.Value,
                OldValues = GetOldValues(entry, action.Value),
                NewValues = GetNewValues(entry, action.Value)
            });
        }

        return result;
    }

    private static AuditAction? GetAuditAction(
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<SoftDeletableEntity> entry)
    {
        if (entry.State == EntityState.Added)
            return AuditAction.Create;

        if (entry.State != EntityState.Modified)
            return null;

        var isDeletedProperty = entry.Property(
            nameof(SoftDeletableEntity.IsDeleted));

        if (isDeletedProperty.IsModified)
        {
            return entry.Entity.IsDeleted
                ? AuditAction.Delete
                : AuditAction.Undelete;
        }

        return AuditAction.Update;
    }

    private static string? GetOldValues(
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<SoftDeletableEntity> entry,
        AuditAction action)
    {
        if (action == AuditAction.Create)
            return null;

        var values = new Dictionary<string, object?>();

        foreach (var property in entry.Properties)
        {
            if (AuditExcludedProperties.Contains(property.Metadata.Name))
                continue;

            if (action is AuditAction.Update &&
                !property.IsModified)
                continue;

            values[property.Metadata.Name] =
                property.OriginalValue;
        }

        return values.Count == 0
            ? null
            : AuditValueSerializer.Serialize(values);
    }

    private static string? GetNewValues(
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<SoftDeletableEntity> entry,
        AuditAction action)
    {
        var values = new Dictionary<string, object?>();

        foreach (var property in entry.Properties)
        {
            if (AuditExcludedProperties.Contains(property.Metadata.Name))
                continue;

            if (action is AuditAction.Update &&
                !property.IsModified)
                continue;

            values[property.Metadata.Name] =
                property.CurrentValue;
        }

        return values.Count == 0
            ? null
            : AuditValueSerializer.Serialize(values);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Load all Fluent API configurations from Infrastructure.
        builder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(SoftDeletableEntity)
                .IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(
                    entityType.ClrType,
                    "e");

                var property = Expression.Property(
                    parameter,
                    nameof(SoftDeletableEntity.IsDeleted));

                var filter = Expression.Lambda(
                    Expression.Equal(
                        property,
                        Expression.Constant(false)),
                    parameter);

                // Hide soft-deleted records from standard queries.
                entityType.SetQueryFilter(filter);
            }
        }
    }

    private sealed class AuditData
    {
        public SoftDeletableEntity Entity { get; set; } = null!;
        public string EntityType { get; set; } = null!;
        public int EntityId { get; set; }
        public AuditAction Action { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
    }
}