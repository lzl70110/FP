using FP.Domain.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FP.Infrastructure.Data.Configurations;

public class DepartmentConfiguration
    : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder
            .HasKey(d => d.Id);

        builder
            .Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(d => d.Notes)
            .HasMaxLength(1000);
    }
}
 
