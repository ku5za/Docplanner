using AutoMapper;
using Docplanner.Application.Models;
using Docplanner.Infrastructure.Models.SlotsRepository;

namespace Docplanner.Infrastructure.Mappers;

public class SlotsRepositoryProfile : Profile
{
    public SlotsRepositoryProfile()
    {
        CreateMap<GetSlotsResponse, WeeklySlots>();
        CreateMap<Infrastructure.Models.SlotsRepository.TimeSlot, Application.Models.TimeSlot>();
        CreateMap<Infrastructure.Models.SlotsRepository.WorkDay, Application.Models.WorkDay>();
        CreateMap<Infrastructure.Models.SlotsRepository.WorkPeriod, Application.Models.WorkPeriod>();
    } 
}