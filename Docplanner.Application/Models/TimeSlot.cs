namespace Docplanner.Application.Models;

public record TimeSlot
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}