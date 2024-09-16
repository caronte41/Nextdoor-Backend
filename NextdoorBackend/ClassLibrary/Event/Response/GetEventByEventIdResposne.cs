using NextDoorBackend.ClassLibrary.Profile.Request;
using NextDoorBackend.ClassLibrary.Profile.Response;

namespace NextDoorBackend.ClassLibrary.Event.Response
{
    public class GetEventByEventIdResposne
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public string OrganizatorName { get; set; }
        public DateTime EventDay { get; set; }
        public TimeSpan EventHour { get; set; }
        public string Address { get; set; }
        public int NeighborhoodId { get; set; }
        public string? Description { get; set; }
        public int InterestedCount { get; set; }
        public int GoingCount { get; set; }
        public ProfileAndIndividualProfileResponse Profile { get; set; }
      
    }
}
