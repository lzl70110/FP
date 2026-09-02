using System.ComponentModel.DataAnnotations;

namespace FP.Web.Models.Forms;

public class NamedActiveEntityViewModel
{
    [Display(Name = "Наименование")]
    public string Name { get; set; } = null!;

    [Display(Name = "Бележки")]
    public string? Notes { get; set; }

    [Display(Name = "Активна")]
    public bool IsActive { get; set; }
}