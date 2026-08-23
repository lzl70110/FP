using FP.Domain.Entities.Requirements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FP.Infrastructure.Data.Configurations;

public class RoomRequirementConfiguration : IEntityTypeConfiguration<RoomRequirement>
{
    public void Configure(EntityTypeBuilder<RoomRequirement> builder)
    {
        builder.HasOne(x => x.Room)
            .WithMany(x => x.RoomRequirements)
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ExtinguisherType)
            .WithMany()
            .HasForeignKey(x => x.ExtinguisherTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.RequiredCount)
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(500);

        builder.HasIndex(x => new
        {
            x.RoomId,
            x.ExtinguisherTypeId
        })
        .IsUnique();
    }
}