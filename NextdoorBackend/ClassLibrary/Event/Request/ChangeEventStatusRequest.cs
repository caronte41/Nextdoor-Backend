namespace NextDoorBackend.ClassLibrary.Event.Request
{
    public class ChangeEventStatusRequest
    {
        public Guid? EventId { get; set; }
        public int? Status { get; set; }
    }
}
