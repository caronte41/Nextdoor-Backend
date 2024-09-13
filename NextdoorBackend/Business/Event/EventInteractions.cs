using Microsoft.EntityFrameworkCore;
using NextDoorBackend.Business.GoogleMaps;
using NextDoorBackend.ClassLibrary.Event.Request;
using NextDoorBackend.ClassLibrary.Event.Response;
using NextDoorBackend.Data;
using NextDoorBackend.SDK.Entities;
using static NextDoorBackend.SDK.Enums.AllMyEnums;

namespace NextDoorBackend.Business.Event
{
    public class EventInteractions : IEventInteractions
    {
        private readonly AppDbContext _context;
        private readonly IGoogleMapsInteractions _googleMapsInteractions;

        public EventInteractions(AppDbContext context, IGoogleMapsInteractions googleMapsInteractions)
        {
            _context = context;
            _googleMapsInteractions = googleMapsInteractions;
        }
        public async Task<CreateEventResponse> CreateEventAsync(CreateEventRequest request)
        {
            // Step 1: Validate the profile exists
            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.Id == request.ProfileId);

            var neighborhoodId = await _googleMapsInteractions.GetNeighborhoodIdByLatLng(request.Latitude, request.Longitude);

            if (profile == null)
            {
                throw new Exception("Profile not found");
            }

            // Step 3: Create the event
            var newEvent = new EventsEntity
            {
                Id = Guid.NewGuid(),
                ProfileId = (Guid)request.ProfileId,
                EventDay = (DateTime)request.EventDay, 
                EventHour = (TimeSpan)request.EventHour,
                EventName = request.EventName,
                OrganizatorName = request.OrganizatorName,
                Address = request.Address,
                NeighborhoodId = (int)neighborhoodId,
                Status = (int)EventStatus.Active,  // Assuming you have an enum for status
            };

            // Step 4: Add the event to the database
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            // Step 5: Return the response
            return new CreateEventResponse
            {
               
            };
        }
        public async Task<ChangeEventStatusResponse> ChangeEventStatus(ChangeEventStatusRequest request)
        {
            var eventEntity = await _context.Events.FindAsync(request.EventId);
            eventEntity.Status = (int)request.Status;
            await _context.SaveChangesAsync();
            return new ChangeEventStatusResponse { };
        }

    }
}
