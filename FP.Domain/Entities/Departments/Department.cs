using FP.Domain.Common;

namespace FP.Domain.Entities.Departments;
public class Department: SoftDeletableEntity
{
    public string Name { get; set; } = null!;
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;

}
