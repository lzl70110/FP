using FP.Domain.Common;
using FP.Domain.Entities.Rooms;
using FP.Domain.Entities.Extinguishers;

namespace FP.Domain.Entities.Requirements;

public class RoomRequirement : SoftDeletableEntity
{
    public int RoomId { get; set; }

    public Room Room { get; set; } = null!;

    public int ExtinguisherTypeId { get; set; }

    public ExtinguisherType ExtinguisherType { get; set; } = null!;

    public int RequiredCount { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; } = true;
}
