namespace NextDoorBackend.ClassLibrary.Event.Request
{
    public class AddParticipantToEventRequest
    {
        public Guid? EventId { get; set; }
        public Guid? ProfileId { get; set; }
        public int? ParticipationStatus { get; set; }
    }
}
