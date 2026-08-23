using FP.Domain.Common;

namespace FP.Domain.Entities.Extinguishers;

public class ExtinguisherType : SoftDeletableEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<Extinguisher> Extinguishers { get; set; }
    = new HashSet<Extinguisher>();
}