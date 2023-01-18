using System.Net;
using TimeWatch.Core;
using TimeWatch.Core.Enums;
using TimeWatch.Core.Interfaces;
using TimeWatch.Core.Models;
using TimeWatch.Service.Extensions;

namespace TimeWatch.Service.Services;

public class TimeWatchService : ITimeWatchService
{
    private readonly ITimeWatchLoginService _timeWatchLoginService;
    private readonly IPunchService _punchService;

    public TimeWatchService(ITimeWatchLoginService timeWatchLoginService, IPunchService punchService)
    {
        _timeWatchLoginService = timeWatchLoginService;
        _punchService = punchService;
    }

    public async Task<HttpStatusCode> PunchIn(TimeWatchRequest request)
    {
        var loginResponse = await _timeWatchLoginService.Login(request);

        if (!_timeWatchLoginService.ValidateLogin(loginResponse)) return HttpStatusCode.Unauthorized;

       var punchResponse = await _punchService.Punch(request, loginResponse, ReportingOptions.PunchIn);

       return punchResponse.StatusCode;
    }

    public async Task<HttpStatusCode> PunchOut(TimeWatchRequest request)
    {
        var loginResponse = await _timeWatchLoginService.Login(request);

        if (!_timeWatchLoginService.ValidateLogin(loginResponse)) return HttpStatusCode.Unauthorized;

        var punchResponse = await _punchService.Punch(request, loginResponse, ReportingOptions.PunchOut);

        return punchResponse.StatusCode;
    }

    public async Task<HttpStatusCode> PunchAll(TimeWatchRequest request)
    {
        var loginResponse = await _timeWatchLoginService.Login(request);

        if (!_timeWatchLoginService.ValidateLogin(loginResponse)) return HttpStatusCode.Unauthorized;

        request.SetDefaultValues();

        var url = GetUrl(request, loginResponse.IxEmployee);

        await _punchService.BeforePunchAll(request, loginResponse, url);

        var workingDays = GetWorkingDays((int)request.Year, (int)request.Month);

        foreach (var day in workingDays)
        {
            var punchResponse = await _punchService.PunchAll(request, loginResponse, day, url);

            if(punchResponse.StatusCode != HttpStatusCode.OK) return punchResponse.StatusCode;

            Thread.Sleep(1000);
        }

        return HttpStatusCode.OK;
    }

    private static List<DateTime> GetWorkingDays(int year, int month)
    {
        var dates = new List<DateTime>();

        for (var date = new DateTime(year, month, 1); date.Month == month; date = date.AddDays(1))
        {
            if (date.DayOfWeek != DayOfWeek.Friday && date.DayOfWeek != DayOfWeek.Saturday)
            {
                dates.Add(date);
            }
        }

        return dates;
    }

    private static string GetUrl(TimeWatchRequest request, string ixEmployee)
    {
        return $"{Consts.TimeWatchBaseUrl}/{Consts.TimeWatchEditUrl}?month={request.Month}&year={request.Year}&teamldr={ixEmployee}&empl_name={request.Company}";
    }
}

