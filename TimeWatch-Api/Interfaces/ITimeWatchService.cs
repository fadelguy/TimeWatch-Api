using System.Net;
using TimeWatch_Api.Models;

namespace TimeWatch_Api.Interfaces;

public interface ITimeWatchService
{
    Task<HttpStatusCode> PunchIn(TimeWatchRequest request);
    Task<HttpStatusCode> PunchOut(TimeWatchRequest request);
}

