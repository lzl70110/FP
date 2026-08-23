using FP.Domain.Common;
using FP.Domain.Entities.Departments;
using FP.Domain.Entities.Requirements;

namespace FP.Domain.Entities.Rooms;

public class Room : SoftDeletableEntity
{
    public string Name { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public string? Notes { get; set; }

    public int DepartmentId { get; set; }

    public Department Department { get; set; } = null!;
    public ICollection<RoomRequirement> RoomRequirements { get; set; }

= new HashSet<RoomRequirement>();
}