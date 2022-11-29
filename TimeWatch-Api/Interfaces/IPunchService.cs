using RestSharp;
using TimeWatch_Api.Enums;
using TimeWatch_Api.Models;

namespace TimeWatch_Api.Interfaces;

public interface IPunchService
{
    Task<RestResponse> Punch(TimeWatchRequest twRequest, TimeWatchLoginResponse loginResponse, ReportingOptions reportingOptions);
    Task BeforePunchAll(TimeWatchRequest twRequest, TimeWatchLoginResponse loginResponse, string url);
    Task<RestResponse> PunchAll(TimeWatchRequest twRequest, TimeWatchLoginResponse loginResponse, DateTime day, string url);
}

