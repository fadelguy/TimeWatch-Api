using RestSharp;
using System.Net;
using TimeWatch_Api.Enums;
using TimeWatch_Api.Interfaces;
using TimeWatch_Api.Models;

namespace TimeWatch_Api.Services;

public class TimeWatchService : ITimeWatchService
{
    private readonly ITimeWatchLoginService _timeWatchLoginService;

    public TimeWatchService(ITimeWatchLoginService timeWatchLoginService)
    {
        _timeWatchLoginService = timeWatchLoginService;
    }

    public async Task<HttpStatusCode> PunchIn(TimeWatchRequest request)
    {
        var loginResponse = await _timeWatchLoginService.Login(request);

        if (!_timeWatchLoginService.ValidateLogin(loginResponse)) return HttpStatusCode.Unauthorized;

       var punchResponse = await Punch(request, loginResponse, ReportingOptions.PunchIn);

       return punchResponse.StatusCode;
    }

    public async Task<HttpStatusCode> PunchOut(TimeWatchRequest request)
    {
        var loginResponse = await _timeWatchLoginService.Login(request);

        if (!_timeWatchLoginService.ValidateLogin(loginResponse)) return HttpStatusCode.Unauthorized;

        var punchResponse = await Punch(request, loginResponse, ReportingOptions.PunchOut);

        return punchResponse.StatusCode;
    }

    private static Task<RestResponse> Punch(TimeWatchRequest twRequest, TimeWatchLoginResponse loginResponse, ReportingOptions reportingOptions)
    {
        var client = new RestClient(Consts.TimeWatchBaseUrl);
        var request = new RestRequest(Consts.TimeWatchPunchUrl, Method.Post);
        request.AddHeader("content-type", "application/x-www-form-urlencoded");
        request.AddHeader("cookie", $"PHPSESSID={loginResponse.Cookie}");
        request.AddHeader("origin", "https://c.timewatch.co.il");
        request.AddHeader("referer", "https://c.timewatch.co.il/punch/punch2.php");
        request.AddParameter("comp", twRequest.Company);
        request.AddParameter("name", twRequest.EmployeeId);
        request.AddParameter("ix", loginResponse.IxEmployee);
        request.AddParameter("type", $"{(int)reportingOptions}");
        request.AddParameter("allowremarks", "1");
        request.AddParameter("msgfound", "0");
        request.AddParameter("thetask", "0");
        request.AddParameter("teamleader", "0");
        request.AddParameter("prevtask", "0");
        request.AddParameter("defaultTask", "0");
        request.AddParameter("withtasks", "0");
        request.AddParameter("restricted", "1");
        request.AddParameter("csrf_token", loginResponse.Token);


        return client.ExecuteAsync(request);
    }
}

