namespace Docplanner.Application.Models;

public record WorkDay
{
    public WorkPeriod WorkPeriod { get; set; }
    public List<TimeSlot> BusySlots { get; set; }
}