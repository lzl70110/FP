using FP.Domain.Common;

namespace FP.Domain.Entities.Departments;
public class Department: AuditableEntity
{
    public string Name { get; set; } = null!;
 
}
