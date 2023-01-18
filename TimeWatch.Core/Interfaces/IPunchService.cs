using RestSharp;
using TimeWatch.Core.Enums;
using TimeWatch.Core.Models;

namespace TimeWatch.Core.Interfaces;

public interface IPunchService
{
    Task<RestResponse> Punch(TimeWatchRequest twRequest, TimeWatchLoginResponse loginResponse, ReportingOptions reportingOptions);
    Task BeforePunchAll(TimeWatchRequest twRequest, TimeWatchLoginResponse loginResponse, string url);
    Task<RestResponse> PunchAll(TimeWatchRequest twRequest, TimeWatchLoginResponse loginResponse, DateTime day, string url);
}

