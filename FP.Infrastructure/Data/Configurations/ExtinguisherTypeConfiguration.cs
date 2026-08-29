using FP.Domain.Entities.Extinguishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FP.Infrastructure.Data.Configurations;

public class ExtinguisherTypeConfiguration : IEntityTypeConfiguration<ExtinguisherType>
{
    public void Configure(EntityTypeBuilder<ExtinguisherType> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.Description)
            .HasMaxLength(500);
    }
}