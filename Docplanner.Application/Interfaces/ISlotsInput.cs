using Docplanner.Application.Models;

namespace Docplanner.Application.Interfaces;

public interface ISlotsInput
{
    public Task<WeeklySlots> GetSlotsAsync(string mondayDate);
}