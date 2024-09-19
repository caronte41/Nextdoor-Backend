using Mapster;
using Microsoft.EntityFrameworkCore;
using NextDoorBackend.Business.GoogleMaps;
using NextDoorBackend.ClassLibrary.Event.Request;
using NextDoorBackend.ClassLibrary.Event.Response;
using NextDoorBackend.ClassLibrary.Post.Response;
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
        public async Task<GetEventByEventIdResposne> GetEventByEventId(GetEventByEventIdRequest request)
        {
            var eventEntity = await _context.Events
       .Where(e => e.Id == request.EventId)
       .Include(e => e.Profile) // Include the base Profile entity first
       .Include(e => e.EventsParticipants) // Include participants for count
       .FirstOrDefaultAsync();

            if (eventEntity == null)
            {
                throw new Exception("Event not found");
            }

            // Check if the profile is associated with an IndividualProfile or BusinessProfile
            var profileId = eventEntity.ProfileId;

            // Check for IndividualProfile
            var individualProfile = await _context.IndividualProfiles
                .Where(ip => ip.Id == profileId)
                .FirstOrDefaultAsync();

            if (individualProfile != null)
            {
                eventEntity.Profile.IndividualProfile = individualProfile;
            }
            else
            {
                // Load BusinessProfile if IndividualProfile is not found
                var businessProfile = await _context.BusinessProfiles
                    .Where(bp => bp.Id == profileId)
                    .FirstOrDefaultAsync();

                if (businessProfile != null)
                {
                    eventEntity.Profile.BusinessProfile = businessProfile;
                }
            }

            // Calculate Interested and Going counts
            var interestedCount = eventEntity.EventsParticipants.Count(p => p.Status == ParticipationStatus.Interested);
            var goingCount = eventEntity.EventsParticipants.Count(p => p.Status == ParticipationStatus.Going);

            // Map the event entity to the response DTO, including the counts
            var response = eventEntity.Adapt<GetEventByEventIdResposne>();

            // Add the interested and going counts to the response
            response.InterestedCount = interestedCount;
            response.GoingCount = goingCount;

            return response;
        }
        public async Task<List<GetAllEventsByProfileIdResponse>> GetAllEventsByProfileId(GetAllEventsByProfileIdRequest request)
        {
            var profile = await _context.Profiles
       .Where(p => p.Id == request.ProfileId)
       .Include(p => p.IndividualProfile) // Ensure you load IndividualProfile to access NeighborhoodId
       .FirstOrDefaultAsync();

            if (profile == null || profile.IndividualProfile == null)
            {
                throw new Exception("Profile or IndividualProfile not found");
            }

            var allEvents = await _context.Events
                .Where(e => e.NeighborhoodId == profile.IndividualProfile.NeighborhoodId)
                .Include(e => e.EventsParticipants) // Include participants to calculate counts
                .Select(e => new GetAllEventsByProfileIdResponse
                {
                    Id = e.Id,
                    EventName = e.EventName,
                    OrganizatorName = e.OrganizatorName,
                    EventDay = e.EventDay,
                    EventHour = e.EventHour,
                    Address = e.Address,
                    NeighborhoodId = e.NeighborhoodId,
                    // Calculate Interested and Going counts
                    InterestedCount = e.EventsParticipants.Count(p => p.Status == ParticipationStatus.Interested),
                    GoingCount = e.EventsParticipants.Count(p => p.Status == ParticipationStatus.Going)
                })
                .ToListAsync();

            return allEvents;
        }
        public async Task<AddParticipantToEventResposne> AddParticipantToEvent(AddParticipantToEventRequest request)
        {
            var eventEntity = await _context.Events
       .Where(p => p.Id == request.EventId)
       .FirstOrDefaultAsync();

            if (eventEntity == null)
            {
                throw new Exception("Event not found");
            }

            // Check if the participant already exists for this event
            var existingParticipant = await _context.EventParticipants
                .FirstOrDefaultAsync(p => p.EventId == request.EventId && p.ProfileId == request.ProfileId);

            if (existingParticipant != null)
            {
                throw new Exception("Participant already exists for this event.");
            }

            // Create the new participant entity
            var newParticipant = new EventsParticipantsEntity
            {
                Id = Guid.NewGuid(),
                EventId = (Guid)request.EventId,
                ProfileId = (Guid)request.ProfileId,
                Status = (ParticipationStatus)(int)request.ParticipationStatus,
                AttendedAt = DateTime.UtcNow
            };

            // Add the participant directly to the EventsParticipants table
            _context.EventParticipants.Add(newParticipant);

            // Save changes
            await _context.SaveChangesAsync();

            return new AddParticipantToEventResposne();
        }


    }
}
