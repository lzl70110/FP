 
using FP.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace FP.Domain.Entities.Extinguishers;

public class ExtinguisherType : SoftDeletableEntity
{
    [Display(Name = "Наименование")]
    [Required(ErrorMessage = "Наименованието е задължително.")]
    [StringLength(30, MinimumLength = 2,
        ErrorMessage = "Наименованието трябва да бъде между 2 и 30 символа.")]
    public string Name { get; set; } = null!;

    [Display(Name = "Описание")]
    [StringLength(500,
        ErrorMessage = "Описанието не може да надвишава 500 символа.")]
    public string? Description { get; set; }

    public ICollection<Extinguisher> Extinguishers { get; set; }
        = new HashSet<Extinguisher>();
}
 
