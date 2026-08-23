using FP.Domain.Entities.Extinguishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FP.Infrastructure.Data.Configurations;
public class ExtinguisherConfiguration : IEntityTypeConfiguration<Extinguisher>
{


    public void Configure(EntityTypeBuilder<Extinguisher> builder)
    {
        builder.HasOne(x => x.ExtinguisherType)
    .WithMany(x => x.Extinguishers)
    .HasForeignKey(x => x.ExtinguisherTypeId)
    .OnDelete(DeleteBehavior.Restrict);

    }
}
