namespace FP.Infrastructure.Services;

public class DateTimeService
{
    private static readonly TimeZoneInfo BulgariaTimeZone =
        TimeZoneInfo.FindSystemTimeZoneById("Europe/Sofia");

    public DateTime UtcToLocal(DateTime utcDateTime)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(
            DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc),
            BulgariaTimeZone);
    }
}