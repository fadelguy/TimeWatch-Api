using Microsoft.Extensions.DependencyInjection;
using TimeWatch.Core.Interfaces;
using TimeWatch.Service.Services;

namespace TimeWatch.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTimeWatchService(this IServiceCollection services) => services
            .AddSingleton<ITimeWatchService, TimeWatchService>()
            .AddSingleton<ITimeWatchLoginService, TimeWatchLoginService>()
            .AddSingleton<IPunchService, PunchService>();

    }
}
