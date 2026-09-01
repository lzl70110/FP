using FP.Domain.Common;
using FP.Domain.Entities.Checkers;
using FP.Domain.Entities.Departments;
using FP.Domain.Entities.Employees;
using FP.Domain.Entities.Extinguishers;
using FP.Domain.Entities.Positions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FP.Infrastructure.Data;

public class AppDbContext : DbContext
{
    // Temporary system user until Identity is introduced.
    private const string SystemUser = "System";

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

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries<SoftDeletableEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:

                    // Set creation audit information.
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedById = SystemUser;
                    break;

                case EntityState.Modified:

                    // Handle soft delete separately from regular updates.
                    if (entry.Entity.IsDeleted)
                    {
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        entry.Entity.DeletedById = SystemUser;
                    }
                    else
                    {
                        // Set update audit information.
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedById = SystemUser;
                    }

                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
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
}
