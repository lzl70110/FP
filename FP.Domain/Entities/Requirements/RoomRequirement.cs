 
using FP.Domain.Common;
using FP.Domain.Entities.Rooms;
using FP.Domain.Entities.Extinguishers;
using System.ComponentModel.DataAnnotations;

namespace FP.Domain.Entities.Requirements;

public class RoomRequirement : SoftDeletableEntity
{
    public int RoomId { get; set; }

    public Room Room { get; set; } = null!;

    public int ExtinguisherTypeId { get; set; }

    public ExtinguisherType ExtinguisherType { get; set; } = null!;

    [Display(Name = "Необходим брой")]
    [Range(1, 30,
        ErrorMessage = "Броят трябва да бъде между 1 и 30.")]
    public int RequiredCount { get; set; }

    [Display(Name = "Забележка")]
    [StringLength(500,
        ErrorMessage = "Забележката не може да надвишава 500 символа.")]
    public string? Notes { get; set; }

    public bool IsActive { get; set; } = true;
}
 
