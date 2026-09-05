namespace FP.Web.Models;

public class DeleteConfirmViewModel
{
    // Уникален идентификатор на модала.
    public string ModalId { get; set; } = "deleteConfirmModal";

    // Идентификатор на заглавието на модала.
    public string ModalIdLabel { get; set; } = "deleteConfirmModalLabel";

    // Текстът, който ще се показва в съобщението за потвърждение.
    public string Message { get; set; } = string.Empty;

    // Контролерът, към който ще бъде изпратена заявката.
    public string Controller { get; set; } = string.Empty;

    // Действието, което ще бъде извикано.
    public string Action { get; set; } = "Delete";

    // Id на записа, който ще бъде изтрит.
    public int Id { get; set; }
}