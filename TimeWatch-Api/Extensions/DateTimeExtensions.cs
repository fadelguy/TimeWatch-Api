namespace TimeWatch_Api.Extensions;

public static class DateTimeExtensions
{
    public static string GetDayFormat(this DateTime day)
    {
        return day.ToString("yyyy-MM-dd");
    }

    public static string GetFirstDayOfMonth(this DateTime day)
    {
        return $"{day.Year}-{day.Month}-1";
    }
}

