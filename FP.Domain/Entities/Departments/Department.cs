using FP.Domain.Common;
using FP.Domain.Entities.Rooms;

namespace FP.Domain.Entities.Departments;
public class Department: SoftDeletableEntity
{
    public string Name { get; set; } = null!;
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Room> Rooms { get; set; }
    = new HashSet<Room>();
}
