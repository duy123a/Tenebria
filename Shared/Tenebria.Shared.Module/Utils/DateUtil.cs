using Tenebria.Shared.Module.Configuration;

namespace Tenebria.Shared.Module.Utils;

public static class DateUtil
{
    private static TimeZoneInfo? _currentTimeZoneInfo;
    private static readonly object Lock = new();

    private static TimeZoneInfo CurrentTimeZoneInfo => GetCurrentTimeZoneInfo();

    public static TimeZoneInfo GetCurrentTimeZoneInfo()
    {
        if (_currentTimeZoneInfo == null)
        {
            lock (Lock)
            {
                if (_currentTimeZoneInfo == null)
                {
                    var tzId = AppSettingProvider.Instance.TimeZone;
                    _currentTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(string.IsNullOrEmpty(tzId) ? "SE Asia Standard Time" : tzId);
                }
            }
        }
        return _currentTimeZoneInfo;
    }

    public static DateTime GetNow()
    {
        var time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, CurrentTimeZoneInfo);
        return DateTime.SpecifyKind(time, DateTimeKind.Unspecified);
    }

    public static DateTime GetToday()
    {
        var today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, CurrentTimeZoneInfo).Date;
        return DateTime.SpecifyKind(today, DateTimeKind.Unspecified);
    }

    public static DateTime ToSystemTimeZone(DateTime dateTime)
    {
        return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, CurrentTimeZoneInfo);
    }

    public static long CurrentUnixTimestamp()
    {
        var time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, CurrentTimeZoneInfo);
        var dto = new DateTimeOffset(time, CurrentTimeZoneInfo.GetUtcOffset(time));
        return dto.ToUnixTimeMilliseconds();
    }

    public static long DateTimeToUnix(DateTime dateTime)
    {
        var time = TimeZoneInfo.ConvertTime(dateTime, CurrentTimeZoneInfo);
        var dto = new DateTimeOffset(time, CurrentTimeZoneInfo.GetUtcOffset(time));
        return dto.ToUnixTimeMilliseconds();
    }

    public static DateTime UnixToDateTime(long unixTimeStampInMilliseconds)
    {
        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStampInMilliseconds);
        var time = TimeZoneInfo.ConvertTimeFromUtc(dateTimeOffset.UtcDateTime, CurrentTimeZoneInfo);
        return time;
    }
}
