namespace NextDoorBackend.ClassLibrary.Event.Request
{
    public class CreateEventRequest
    {
        public Guid? ProfileId { get; set; }
        public int? NeighborhoodId { get; set; }
        public DateTime? EventDay { get; set; } // Date of the event
        public TimeSpan? EventHour { get; set; } // Time of the event
        public string? EventName { get; set; } // Name of the event
        public string? OrganizatorName { get; set; } // Name of the organizer
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
