using FP.Domain.Common;
using FP.Domain.Entities.Audit;

namespace FP.Application.Contracts.Services;

public interface IAuditService
{
    AuditEntry CreateEntry(
        AuditAction action,
        string entityType,
        int entityId,
        string? oldValues,
        string? newValues);
}