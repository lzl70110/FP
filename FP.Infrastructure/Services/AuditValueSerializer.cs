using System.Text.Json;

namespace FP.Infrastructure.Services;

public static class AuditValueSerializer
{
    public static string Serialize(
        IDictionary<string, object?> values)
    {
        return JsonSerializer.Serialize(values);
    }
}