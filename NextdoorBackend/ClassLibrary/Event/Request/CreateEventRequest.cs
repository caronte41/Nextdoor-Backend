namespace NextDoorBackend.ClassLibrary.Event.Request
{
    public class CreateEventRequest
    {
        public Guid? ProfileId { get; set; }
        public int? NeighborhoodId { get; set; }
        public DateTime? EventDay { get; set; } 
        public TimeSpan? EventHour { get; set; }
        public DateTime? EventEndDay { get; set; }
        public TimeSpan? EventEndHour { get; set; }
        public byte[]? CoverPhoto { get; set; }
        public string? EventName { get; set; }
        public string? Description { get; set; }

        public string? OrganizatorName { get; set; } 
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
