namespace Docplanner.Infrastructure.Models.SlotsRepository;

public record WorkDay
{
    public WorkPeriod WorkPeriod { get; set; }
    public List<TimeSlot> BusySlots { get; set; }
}