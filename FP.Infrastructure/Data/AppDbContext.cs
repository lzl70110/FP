using FP.Domain.Entities.Departments;
using FP.Domain.Entities.Employees;
using FP.Domain.Entities.Checkers;
using Microsoft.EntityFrameworkCore;
using FP.Domain.Entities.Extinguishers;

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

    public DbSet<Checker> Checkers { get; set; } = null!;
    public DbSet<Extinguisher> Extinguishers { get; set; } = null!;
    public DbSet<ExtinguisherType> ExtinguisherTypes { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
    }
}