using Microsoft.EntityFrameworkCore;
using NextDoorBackend.Data;
using Quartz;
using System;
using System.Threading.Tasks;
using static NextDoorBackend.SDK.Enums.AllMyEnums;

public class UpdateEventStatusJob : IJob
{
    private readonly AppDbContext _context;

    public UpdateEventStatusJob(AppDbContext context)
    {
        _context = context;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        // Get the current date (UTC or local depending on your event time zone)
        var currentDate = DateTime.UtcNow;

        // Find events with active status and past event day
        var eventsToUpdate = await _context.Events
            .Where(e => e.Status == (int)EventStatus.Active && e.EventDay < currentDate)
            .ToListAsync();

        // Update their status to 'Completed'
        foreach (var eventEntity in eventsToUpdate)
        {
            eventEntity.Status = (int)EventStatus.Completed;
        }

        // Save the changes to the database
        await _context.SaveChangesAsync();
    }
}
