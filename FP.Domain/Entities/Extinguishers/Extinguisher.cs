using FP.Domain.Common;

namespace FP.Domain.Entities.Extinguishers;
public class Extinguisher : SoftDeletableEntity
{
    public int ExtinguisherTypeId { get; set; }

    public ExtinguisherType ExtinguisherType { get; set; } = null!;
}
