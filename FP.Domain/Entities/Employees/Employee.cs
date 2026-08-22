using FP.Domain.Common;
using FP.Domain.Entities.Departments;

namespace FP.Domain.Entities.Employees;

public class Employee : AuditableEntity
{
    public string WorkNumber { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string Position { get; set; } = null!;

    public int DepartmentId { get; set; }

    public Department Department { get; set; } = null!;
}