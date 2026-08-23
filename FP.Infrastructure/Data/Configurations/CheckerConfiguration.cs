using FP.Domain.Entities.Checkers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FP.Infrastructure.Data.Configurations;

public class CheckerConfiguration : IEntityTypeConfiguration<Checker>
{
    public void Configure(EntityTypeBuilder<Checker> builder)
    {
        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.EmployeeId)
            .IsUnique();
    }
}
