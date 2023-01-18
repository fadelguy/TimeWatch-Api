using System.Net;
using TimeWatch.Core.Models;

namespace TimeWatch.Core.Interfaces;

public interface ITimeWatchService
{
    Task<HttpStatusCode> PunchIn(TimeWatchRequest request);
    Task<HttpStatusCode> PunchOut(TimeWatchRequest request);
    Task<HttpStatusCode> PunchAll(TimeWatchRequest request);
}

