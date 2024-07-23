using System.Globalization;
using Docplanner.Application.Interfaces;
using Docplanner.Application.Models;

namespace Docplanner.Application;

public class SlotsService(ISlotsInput slotsInput) : ISlotsService
{
    public async Task<GetAvailableSlotsResponse> GetAvailableSlots(GetAvailableSlotsQuery query)
    {
        var slots = await slotsInput.GetSlotsAsync(query.MondayDate);
        var mondayDate = DateOnly.ParseExact(query.MondayDate, "yyyyMMdd", CultureInfo.InvariantCulture);

        return new GetAvailableSlotsResponse
        {
            Monday = GetAvailableTimeSlotsPerDay(slots.SlotDurationMinutes, slots.Monday, mondayDate),
            Tuesday = GetAvailableTimeSlotsPerDay(slots.SlotDurationMinutes, slots.Tuesday, mondayDate.AddDays(1)),
            Wednesday = GetAvailableTimeSlotsPerDay(slots.SlotDurationMinutes, slots.Wednesday, mondayDate.AddDays(2)),
            Thursday = GetAvailableTimeSlotsPerDay(slots.SlotDurationMinutes, slots.Thursday, mondayDate.AddDays(3)),
            Friday = GetAvailableTimeSlotsPerDay(slots.SlotDurationMinutes, slots.Friday, mondayDate.AddDays(4)),
            Saturday = GetAvailableTimeSlotsPerDay(slots.SlotDurationMinutes, slots.Saturday, mondayDate.AddDays(5)),
            Sunday = GetAvailableTimeSlotsPerDay(slots.SlotDurationMinutes, slots.Sunday, mondayDate.AddDays(6))
        };
    }

    private List<TimeSlot>? GetAvailableTimeSlotsPerDay(ushort timeDuration, WorkDay? workday, DateOnly currentDayDate)
    {
        if (workday is null)
        {
            return null;
        }
        
        var slotTimeSpan = new TimeSpan(timeDuration / 60, timeDuration % 60, 0);
        var workStartTime = new TimeOnly(workday.WorkPeriod.StartHour, 0);
        var lunchBrakeStartTime = new TimeOnly(workday.WorkPeriod.LunchStartHour, 0);
        var postLunchStartTime = new TimeOnly(workday.WorkPeriod.LunchEndHour, 0);
        var workEndTime = new TimeOnly(workday.WorkPeriod.EndHour, 0);

        var possiblePreLunchTimeSlots = 
            GetTimeSlotsRange(workStartTime, lunchBrakeStartTime, slotTimeSpan, currentDayDate);
        var possiblePostLunchTimeSlots =
            GetTimeSlotsRange(postLunchStartTime, workEndTime, slotTimeSpan, currentDayDate);

        return possiblePreLunchTimeSlots
            .Concat(possiblePostLunchTimeSlots)
            .Where(timeSpan => !workday.BusySlots.Any(x => x.Start == timeSpan.Start && x.End == timeSpan.End))
            .ToList();
    }

    private IEnumerable<TimeSlot> GetTimeSlotsRange(TimeOnly rangeStart, TimeOnly rangeEnd, TimeSpan timeSpan, DateOnly date)
    {
        var currentTimeSlotStart = rangeStart;
        while (currentTimeSlotStart < rangeEnd)
        {
            yield return new TimeSlot()
            {
                Start = new DateTime(date, currentTimeSlotStart),
                End = new DateTime(date, currentTimeSlotStart.Add(timeSpan))
            };

            currentTimeSlotStart = currentTimeSlotStart.Add(timeSpan);
        }
    }
}