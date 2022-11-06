using TimeWatch_Api.Models;

namespace TimeWatch_Api.Interfaces;

    public interface ITimeWatchLoginService
    {
        Task<TimeWatchLoginResponse> Login(TimeWatchRequest twRequest);
        bool ValidateLogin(TimeWatchLoginResponse loginResponse);
    }

