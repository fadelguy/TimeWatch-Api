using Microsoft.AspNetCore.Mvc;
using System.Net;
using TimeWatch.Core.Interfaces;
using TimeWatch.Core.Models;

namespace TimeWatch_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeWatchController : ControllerBase
{
    private readonly ITimeWatchService _timeWatchService;

    private readonly ILogger<TimeWatchController> _logger;

    public TimeWatchController(ILogger<TimeWatchController> logger, ITimeWatchService timeWatchService)
    {
        _logger = logger;
        _timeWatchService = timeWatchService;
    }

    [HttpPost("punchIn")]
    public Task<HttpStatusCode> PunchIn(TimeWatchRequest request)
    {
        return _timeWatchService.PunchIn(request);
       
    }

    [HttpPost("punchOut")]
    public Task<HttpStatusCode> PunchOut(TimeWatchRequest request)
    {
        return _timeWatchService.PunchOut(request);
    }

    [HttpPost("punchAll")]
    public Task<HttpStatusCode> PunchAll(TimeWatchRequest request)
    {
        return _timeWatchService.PunchAll(request);
    }
}
