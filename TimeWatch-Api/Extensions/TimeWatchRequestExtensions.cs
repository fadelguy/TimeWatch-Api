using TimeWatch_Api.Models;

namespace TimeWatch_Api.Extensions;

public static class TimeWatchRequestExtensions
{
    public static void SetDefaultValues(this TimeWatchRequest request)
    {
        request.Year ??= DateTime.Now.Year;
        request.Month ??= DateTime.Now.Month;
        request.StartHour ??= "09";
        request.EndHour ??= "18";
    }
}

