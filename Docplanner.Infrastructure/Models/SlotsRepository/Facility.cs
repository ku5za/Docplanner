namespace Docplanner.Infrastructure.Models.SlotsRepository;

public record Facility
{
    public Guid FacilityId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}