 
using FP.Domain.Common;
using FP.Domain.Entities.Positions;
using FP.Domain.Entities.Rooms;
using System.ComponentModel.DataAnnotations;

namespace FP.Domain.Entities.Departments;

public class Department : SoftDeletableEntity
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

    public bool IsActive { get; set; } = true;

    public ICollection<Position> Positions { get; set; }
        = new HashSet<Position>();

    public ICollection<Room> Rooms { get; set; }
        = new HashSet<Room>();
}
 
