namespace Docplanner.Application.Models;

public class GetAvailableSlotsResponse
{
    public List<TimeSlot> Monday { get; set; }
    public List<TimeSlot>? Tuesday { get; set; }
    public List<TimeSlot>? Wednesday { get; set; }
    public List<TimeSlot>? Thursday { get; set; }
    public List<TimeSlot>? Friday { get; set; }
    public List<TimeSlot> Saturday { get; set; }
    public List<TimeSlot> Sunday { get; set; }
}