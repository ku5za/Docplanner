namespace Docplanner.Application.Models;

public record WorkPeriod
{
    public byte StartHour { get; set; }
    public byte EndHour { get; set; }
    public byte LunchStartHour { get; set; }
    public byte LunchEndHour { get; set; }
}