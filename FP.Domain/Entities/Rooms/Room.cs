 
using FP.Domain.Common;
using FP.Domain.Entities.Departments;
using FP.Domain.Entities.Requirements;
using System.ComponentModel.DataAnnotations;

namespace FP.Domain.Entities.Rooms;

public class Room : SoftDeletableEntity
{
    [Display(Name = "Наименование")]
    [Required(ErrorMessage = "Наименованието е задължително.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Наименованието трябва да бъде между 2 и 100 символа.")]
    public string Name { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    [Display(Name = "Забележка")]
    [StringLength(150,
        ErrorMessage = "Забележката не може да надвишава 150 символа.")]
    public string? Notes { get; set; }

    public int DepartmentId { get; set; }

    public Department Department { get; set; } = null!;

    public ICollection<RoomRequirement> RoomRequirements { get; set; }
        = new HashSet<RoomRequirement>();
}
 