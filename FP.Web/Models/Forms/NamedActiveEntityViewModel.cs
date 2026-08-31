namespace FP.Web.Models.Forms;

public class NamedActiveEntityViewModel
{
    public string Name { get; set; } = null!;

    public string? Notes { get; set; }

    public bool IsActive { get; set; }
}