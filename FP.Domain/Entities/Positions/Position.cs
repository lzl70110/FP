 
using FP.Domain.Common;
using FP.Domain.Entities.Departments;
using FP.Domain.Entities.Employees;
using System.ComponentModel.DataAnnotations;

namespace FP.Domain.Entities.Positions;

public class Position : SoftDeletableEntity
{
    [Display(Name = "Наименование")]
    [Required(ErrorMessage = "Наименованието е задължително.")]
    [StringLength(
        100,
        MinimumLength = 2,
        ErrorMessage = "Наименованието трябва да бъде между 2 и 100 символа.")]
    public string Name { get; set; } = null!;

    [Display(Name = "Забележка")]
    [StringLength(
        1000,
        ErrorMessage = "Забележката не може да надвишава 1000 символа.")]
    public string? Notes { get; set; }

    [Display(Name = "Активна")]
    public bool IsActive { get; set; } = true;
 

    public int DepartmentId { get; set; }

    public Department Department { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; }
        = new HashSet<Employee>();
}
 
