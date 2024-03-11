using Microsoft.Extensions.DependencyInjection;
using System.Net;
using TimeWatch.Core.Interfaces;
using TimeWatch.Core.Models;
using TimeWatch.Service;

namespace TimeWatch_ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var timeWatchRequest = GetTimeWatchRequest();

            var result = RunCommand(timeWatchRequest);

            Console.WriteLine($"result: {result.Result}");
            Console.ReadLine();
        }


        private static async Task<HttpStatusCode> RunCommand(TimeWatchDetails timeWatchDetails)
        {
            var timeWatchService = GetTimeWatchService();

            var timeWatchRequest = ConvertToTimeWatchRequest(timeWatchDetails);

            switch (timeWatchDetails.Command.ToLower())
            {
                case "1":
                case "punchin":
                    return await timeWatchService.PunchIn(timeWatchRequest);
                case "2":
                case "punchout":
                    return await timeWatchService.PunchOut(timeWatchRequest);
                case "3":
                case "punchall":
                    Console.WriteLine("Working...");
                    return await timeWatchService.PunchAll(timeWatchRequest);
                default:
                    return HttpStatusCode.BadRequest;
            }
        }

        private static ITimeWatchService GetTimeWatchService()
        {
            var serviceProvider = new ServiceCollection()
                .AddTimeWatchService()
                .BuildServiceProvider();

            return serviceProvider.GetService<ITimeWatchService>();
        }

        private static TimeWatchDetails GetTimeWatchRequest()
        {
            var timeWatchDetails = new TimeWatchDetails();

            Console.WriteLine("Choose command (by number):");
            Console.WriteLine("1. PunchIn");
            Console.WriteLine("2. PunchOut");
            Console.WriteLine("3. PunchAll");
            timeWatchDetails.Command = Console.ReadLine();

            Console.WriteLine("Enter company id:");
            timeWatchDetails.Company = Console.ReadLine();

            Console.WriteLine("Enter employee id:");
            timeWatchDetails.EmployeeId = Console.ReadLine();

            Console.WriteLine("Enter password:");
            timeWatchDetails.Password = Console.ReadLine();

            if (timeWatchDetails.Command == "3")
            {
                Console.WriteLine("Enter start hour (default is 09):");
                var startHour = Console.ReadLine();
                timeWatchDetails.StartHour = string.IsNullOrEmpty(startHour) ? null : startHour;

                Console.WriteLine("Enter end hour (default is 18):");
                var endHour = Console.ReadLine();
                timeWatchDetails.EndHour = string.IsNullOrEmpty(endHour) ? null : endHour;

                Console.WriteLine($"Enter year (default is {DateTime.Now.Year}):");
                if (int.TryParse(Console.ReadLine(), out int year))
                    timeWatchDetails.Year = year;

                Console.WriteLine($"Enter month (default is {DateTime.Now.Month}):");
                if (int.TryParse(Console.ReadLine(), out int month))
                    timeWatchDetails.Month = month;
            }

            return timeWatchDetails;
        }

        private static TimeWatchRequest ConvertToTimeWatchRequest(TimeWatchDetails timeWatchDetails)
        {
            return new TimeWatchRequest
            {
                Company = timeWatchDetails.Company,
                EmployeeId = timeWatchDetails.EmployeeId,
                Password = timeWatchDetails.Password,
                StartHour = timeWatchDetails.StartHour,
                EndHour = timeWatchDetails.EndHour,
                Year = timeWatchDetails.Year,
                Month = timeWatchDetails.Month
            };
        }


    }
}