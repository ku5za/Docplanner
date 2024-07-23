namespace Docplanner.Application.Models;

public class WeeklySlots
{
    public ushort SlotDurationMinutes { get; set; }
    public WorkDay? Monday { get; set; }
    public WorkDay? Tuesday { get; set; }
    public WorkDay? Wednesday { get; set; }
    public WorkDay? Thursday { get; set; }
    public WorkDay? Friday { get; set; }
    public WorkDay? Saturday { get; set; }
    public WorkDay? Sunday { get; set; }
}