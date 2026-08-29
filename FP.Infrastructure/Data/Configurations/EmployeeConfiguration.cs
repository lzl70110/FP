using FP.Domain.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FP.Infrastructure.Data.Configurations;

public class EmployeeConfiguration
    : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.WorkNumber)
            .IsRequired()
            .HasMaxLength(4);

        builder
            .Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(e => e.MiddleName)
            .HasMaxLength(100);

        builder
            .Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(e => e.Notes)
            .HasMaxLength(1000);

        builder
            .HasOne(e => e.Department)
            .WithMany()
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Position)
            .WithMany(p => p.Employees)
            .HasForeignKey(e => e.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasIndex(e => e.WorkNumber)
            .IsUnique();
    }
}