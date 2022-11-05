using TimeWatch_Api.Models;

namespace TimeWatch_Api.Interfaces;

public interface ITimeWatchService
{
    void PunchIn(TimeWatchRequest request);
    void PunchOut(TimeWatchRequest request);
}

