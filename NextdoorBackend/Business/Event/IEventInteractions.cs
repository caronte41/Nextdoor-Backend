using NextDoorBackend.ClassLibrary.Event.Request;
using NextDoorBackend.ClassLibrary.Event.Response;

namespace NextDoorBackend.Business.Event
{
    public interface IEventInteractions
    {
        Task<CreateEventResponse> CreateEventAsync(CreateEventRequest request);
        Task<ChangeEventStatusResponse> ChangeEventStatus(ChangeEventStatusRequest request);
        Task<GetEventByEventIdResposne> GetEventByEventId(GetEventByEventIdRequest request);
        Task<List<GetAllEventsByProfileIdResponse>> GetAllEventsByProfileId(GetAllEventsByProfileIdRequest request);
        Task<AddParticipantToEventResposne> AddParticipantToEvent(AddParticipantToEventRequest request);
        Task<List<GetAllEventsByProfileIdResponse>> GetUsersEvents(GetAllEventsByProfileIdRequest request);
    }
}
