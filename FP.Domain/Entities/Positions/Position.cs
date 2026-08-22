using FP.Domain.Common;
using FP.Domain.Entities.Employees;

namespace FP.Domain.Entities.Positions;

public class Position : SoftDeletableEntity
{
    public string Name { get; set; } = null!;

    public string? Notes { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Employee> Employees { get; set; }
        = new HashSet<Employee>();
}