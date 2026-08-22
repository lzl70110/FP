using FP.Domain.Common;
using FP.Domain.Entities.Employees;

namespace FP.Domain.Entities.Checkers;

public class Checker : SoftDeletableEntity
{
    public int EmployeeId { get; set; }

    public Employee Employee { get; set; } = null!;
}