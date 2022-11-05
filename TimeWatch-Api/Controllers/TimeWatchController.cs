using Microsoft.AspNetCore.Mvc;
using TimeWatch_Api.Interfaces;
using TimeWatch_Api.Models;

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
    public IActionResult PunchIn(TimeWatchRequest request)
    {
        _timeWatchService.PunchIn(request);
        return Ok();
    }

    [HttpPost("punchOut")]
    public IActionResult PunchOut(TimeWatchRequest request)
    {
        _timeWatchService.PunchOut(request);
        return Ok();
    }
}
