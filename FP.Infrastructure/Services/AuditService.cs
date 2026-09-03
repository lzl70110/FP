using FP.Application.Contracts.Services;
using FP.Domain.Common;
using FP.Domain.Entities.Audit;

namespace FP.Infrastructure.Services;

public class AuditService : IAuditService
{
    public AuditEntry CreateEntry(
        AuditAction action,
        string entityType,
        int entityId,
        string? oldValues,
        string? newValues)
    {
        return new AuditEntry
        {
            Timestamp = DateTime.UtcNow,
            UserId = "System",
            EntityType = entityType,
            EntityId = entityId,
            Action = action,
            OldValues = oldValues,
            NewValues = newValues
        };
    }
}