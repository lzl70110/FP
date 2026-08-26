using FP.Domain.Common;
using FP.Domain.Entities.Rooms;
using System.ComponentModel.DataAnnotations;

namespace FP.Domain.Entities.Departments;
public class Department: SoftDeletableEntity
{
    [Display(Name = "Наименование")]
  
    public string Name { get; set; } = null!;
    [Display(Name = "Забележка")]
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Room> Rooms { get; set; }
    = new HashSet<Room>();
    
}
