namespace Docplanner.Infrastructure.Models.SlotsRepository;

public record TimeSlot
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}