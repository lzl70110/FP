using FP.Domain.Common;
using FP.Domain.Entities.Departments;
using FP.Domain.Entities.Positions;

namespace FP.Domain.Entities.DepartmentPositions;

public class DepartmentPosition : AuditableEntity
{
    public int DepartmentId { get; set; }

    public Department Department { get; set; } = null!;

    public int PositionId { get; set; }

    public Position Position { get; set; } = null!;
}