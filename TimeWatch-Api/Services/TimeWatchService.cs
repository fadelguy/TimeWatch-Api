using RestSharp;
using System.Text.RegularExpressions;
using TimeWatch_Api.Enums;
using TimeWatch_Api.Interfaces;
using TimeWatch_Api.Models;

namespace TimeWatch_Api.Services;

public class TimeWatchService : ITimeWatchService
{
    private const string TimeWatchBaseUrl = "https://c.timewatch.co.il";
    private const string TimeWatchLoginUrl = "user/validate_user.php";
    private const string? TimeWatchPunchUrl = "punch/punch3.php";

    public void PunchIn(TimeWatchRequest request)
    {
        var loginResponse = Login(request);

        Punch(request, loginResponse, ReportingOptions.PunchIn);
    }

    public void PunchOut(TimeWatchRequest request)
    {
        var loginResponse = Login(request);

        Punch(request, loginResponse, ReportingOptions.PunchOut);
    }

    private TimeWatchLoginResponse Login(TimeWatchRequest twRequest)
    {
        var client = new RestClient(TimeWatchBaseUrl);
        var request = new RestRequest(TimeWatchLoginUrl, Method.Post);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("comp", twRequest.Company);
        request.AddParameter("name", twRequest.EmployeeId);
        request.AddParameter("pw", twRequest.Password);
        var response = client.Execute(request);

        var cookie = response.Cookies.First().Value;

        Regex ixEmploeeRegex = new Regex("<input(?:.*?)id=\\\"ixemplee\\\"(?:.*)value=\\\"([^\"]+).*>");
        Regex tokenRegex = new Regex("<input(?:.*?)name=csrf_token(?:.*)value=\\\"([^\"]+).*>");

        var emploeeMatch = ixEmploeeRegex.Match(response.Content);
        var ixEmploeeValue = emploeeMatch.Groups[1].Value;

        var tokenMatch = tokenRegex.Match(response.Content);
        var tokenValue = tokenMatch.Groups[1].Value;

        return new TimeWatchLoginResponse
        {
            Cookie = cookie,
            IxEmployee = ixEmploeeValue,
            Token = tokenValue
        };
    }

    private void Punch(TimeWatchRequest twRequest, TimeWatchLoginResponse loginResponse, ReportingOptions reportingOptions)
    {
        var client = new RestClient(TimeWatchBaseUrl);
        var request = new RestRequest(TimeWatchPunchUrl, Method.Post);
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


        var response = client.Execute(request);
    }
}

