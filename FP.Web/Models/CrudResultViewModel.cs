namespace FP.Web.Models;

public class CrudResultViewModel
{
    public CrudResultType Type { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
}