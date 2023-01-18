using TimeWatch.Core.Models;

namespace TimeWatch.Core.Interfaces;

    public interface ITimeWatchLoginService
    {
        Task<TimeWatchLoginResponse> Login(TimeWatchRequest twRequest);
        bool ValidateLogin(TimeWatchLoginResponse loginResponse);
    }

