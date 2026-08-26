using FP.Domain.Entities.DepartmentPositions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FP.Infrastructure.Data.Configurations;

public class DepartmentPositionConfiguration
    : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.ToTable("DepartmentPositions");

        builder.HasKey(dp => dp.Id);

        builder
            .HasOne(dp => dp.Department)
            .WithMany()
            .HasForeignKey(dp => dp.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(dp => dp.Position)
            .WithMany()
            .HasForeignKey(dp => dp.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasIndex(dp => new { dp.DepartmentId, dp.PositionId })
            .IsUnique();
    }
}