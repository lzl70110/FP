namespace FP.Domain.Common;
public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedById { get; set; }

   
}