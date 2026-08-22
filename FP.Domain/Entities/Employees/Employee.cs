using FP.Domain.Common;
using FP.Domain.Entities.Departments;
using FP.Domain.Entities.Positions;

namespace FP.Domain.Entities.Employees;

public class Employee : SoftDeletableEntity
{
    public string WorkNumber { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public int DepartmentId { get; set; }

    public Department Department { get; set; } = null!;

    public int PositionId { get; set; }

    public Position Position { get; set; } = null!;

    public string? Notes { get; set; }

    public bool IsActive { get; set; } = true;
}