using FP.Domain.Common;
using FP.Domain.Entities.Checkers;
using FP.Domain.Entities.DepartmentPositions;
using FP.Domain.Entities.Departments;
using FP.Domain.Entities.Employees;
using FP.Domain.Entities.Extinguishers;
using FP.Domain.Entities.Positions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FP.Infrastructure.Data;

public class AppDbContext : DbContext
{
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
    public DbSet<DepartmentPosition> DepartmentPositions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(SoftDeletableEntity).IsAssignableFrom(entityType.ClrType))
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

                entityType.SetQueryFilter(filter);
            }
        }
    }
}