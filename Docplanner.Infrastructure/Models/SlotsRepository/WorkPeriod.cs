namespace Docplanner.Infrastructure.Models.SlotsRepository;

public record WorkPeriod
{
    public byte StartHour { get; set; }
    public byte EndHour { get; set; }
    public byte LunchStartHour { get; set; }
    public byte LunchEndHour { get; set; }
}