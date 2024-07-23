using System.Text.Json;
using AutoMapper;
using Docplanner.Application.Models;
using Docplanner.Infrastructure.Helpers;
using Docplanner.Infrastructure.Interfaces;
using Docplanner.Infrastructure.Models.SlotsRepository;

namespace Docplanner.Infrastructure;

public class SlotsRepository(IHttpClientFactory clientFactory, IMapper mapper) : ISlotsRepository
{
    public async Task<WeeklySlots> GetSlotsAsync(string mondayDate)
    {
        using var client = clientFactory.CreateClient(HttpClients.SlotsService);
        var response = await client.GetAsync($"{SlotsServiceEndpoints.GetWeeklyAvailability}/{mondayDate}");
        
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<GetSlotsResponse>(responseContent);

        return mapper.Map<WeeklySlots>(result);
    }
}