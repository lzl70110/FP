
using FP.Domain.Entities.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FP.Infrastructure.Data.Configurations;

public class RoomConfiguration
    : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");
        builder.HasKey(r => r.Id);

        builder
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(r => r.Notes)
            .HasMaxLength(150);

        builder
            .HasOne(r => r.Department)
            .WithMany(d => d.Rooms)
            .HasForeignKey(r => r.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}