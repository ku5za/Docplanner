namespace Docplanner.Application.Models;

public record GetAvailableSlotsQuery
{
    public string MondayDate { get; set; }
}