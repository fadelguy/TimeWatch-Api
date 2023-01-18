using RestSharp;
using TimeWatch.Core;
using TimeWatch.Core.Enums;
using TimeWatch.Core.Interfaces;
using TimeWatch.Core.Models;
using TimeWatch.Service.Extensions;

namespace TimeWatch.Service.Services;

public class PunchService : IPunchService
{
    public Task<RestResponse> Punch(TimeWatchRequest twRequest, TimeWatchLoginResponse loginResponse, ReportingOptions reportingOptions)
    {
        var client = new RestClient(Consts.TimeWatchBaseUrl);
        var request = new RestRequest(Consts.TimeWatchPunchUrl, Method.Post);
        request.AddHeader("content-type", "application/x-www-form-urlencoded");
        request.AddHeader("cookie", $"PHPSESSID={loginResponse.Cookie}");
        request.AddHeader("origin", Consts.TimeWatchBaseUrl);
        request.AddHeader("referer", $"https://c.timewatch.co.il/punch/punch2.php");
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

    public Task BeforePunchAll(TimeWatchRequest twRequest, TimeWatchLoginResponse loginResponse, string url)
    {
        var client = new RestClient(Consts.TimeWatchBaseUrl);
        var request = new RestRequest(url);
        request.AddHeader("content-type", "application/x-www-form-urlencoded");
        request.AddHeader("cookie", $"PHPSESSID={loginResponse.Cookie}");
        request.AddHeader("origin", Consts.TimeWatchBaseUrl);
        request.AddHeader("referer", url);

        return client.ExecuteAsync(request);
    }

    public Task<RestResponse> PunchAll(TimeWatchRequest twRequest, TimeWatchLoginResponse loginResponse, DateTime day, string url)
    {
        var client = new RestClient(Consts.TimeWatchBaseUrl);
        var request = new RestRequest(Consts.TimeWatchPunchAllUrl, Method.Post);
        request.AddHeader("content-type", "application/x-www-form-urlencoded");
        request.AddHeader("cookie", $"PHPSESSID={loginResponse.Cookie}");
        request.AddHeader("origin", Consts.TimeWatchBaseUrl);
        request.AddHeader("referer", url);
        request.AddParameter("c", twRequest.Company);
        request.AddParameter("e", loginResponse.IxEmployee);
        request.AddParameter("tl", loginResponse.IxEmployee);
        request.AddParameter("d", day.GetDayFormat());
        request.AddParameter("jd", day.GetFirstDayOfMonth());
        request.AddParameter("atypehidden", "0");
        request.AddParameter("inclcontracts", "1");
        request.AddParameter("job", "42400");
        request.AddParameter("allowabsence", "3");
        request.AddParameter("allowremarks", "1");
        request.AddParameter("emm0", "00");
        request.AddParameter("ehh0", twRequest.StartHour?.PadLeft(2, '0'));
        request.AddParameter("xmm0", "00");
        request.AddParameter("xhh0", twRequest.EndHour?.PadLeft(2, '0'));
        request.AddParameter("task0", "0");
        request.AddParameter("what0", "1");
        request.AddParameter("task1", "0");
        request.AddParameter("what1", "1");
        request.AddParameter("task2", "0");
        request.AddParameter("what2", "1");
        request.AddParameter("task3", "0");
        request.AddParameter("what3", "1");
        request.AddParameter("task4", "0");
        request.AddParameter("what4", "1");
        request.AddParameter("excuse", "0");
        request.AddParameter("csrf_token", loginResponse.Token);


        return client.ExecuteAsync(request);
    }
}

