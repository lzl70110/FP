using FP.Domain.Common;

namespace FP.Domain.Entities.Audit;

public class AuditEntry : BaseEntity
{
    public DateTime Timestamp { get; set; }

    public string? UserId { get; set; }

    public string EntityType { get; set; } = null!;

    public int EntityId { get; set; }

    public AuditAction Action { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }
}