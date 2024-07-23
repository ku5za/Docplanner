using Docplanner.Application.Models;

namespace Docplanner.Application.Interfaces;

public interface ISlotsService
{
    public Task<GetAvailableSlotsResponse> GetAvailableSlots(GetAvailableSlotsQuery query);
}
