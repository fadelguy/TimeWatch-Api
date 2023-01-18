namespace TimeWatch.Core.Models;

public class TimeWatchRequest
{
    public string EmployeeId { get; set; }
    public string Company { get; set; }
    public string Password { get; set; }
    public string? StartHour { get; set; }
    public string? EndHour { get; set; }
    public int? Year { get; set; }
    public int? Month { get; set; }
    public string Command { get; set; }
}

