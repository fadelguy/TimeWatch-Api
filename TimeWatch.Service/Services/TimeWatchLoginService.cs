using System.Net;
using System.Text.RegularExpressions;
using RestSharp;
using TimeWatch.Core;
using TimeWatch.Core.Interfaces;
using TimeWatch.Core.Models;

namespace TimeWatch.Service.Services;

public class TimeWatchLoginService : ITimeWatchLoginService
{
    public async Task<TimeWatchLoginResponse> Login(TimeWatchRequest twRequest)
    {
        var client = new RestClient(Consts.TimeWatchBaseUrl);
        var request = new RestRequest(Consts.TimeWatchLoginUrl, Method.Post);

        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("comp", twRequest.Company);
        request.AddParameter("name", twRequest.EmployeeId);
        request.AddParameter("pw", twRequest.Password);

        var response = await client.ExecuteAsync(request);

        var cookie = Enumerable.First<Cookie>(response.Cookies).Value;

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

    public bool ValidateLogin(TimeWatchLoginResponse loginResponse)
    {
        return !string.IsNullOrEmpty(loginResponse.Cookie) && 
               !string.IsNullOrEmpty(loginResponse.IxEmployee) &&
               !string.IsNullOrEmpty(loginResponse.Token);
    }
}

