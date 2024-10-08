namespace NextDoorBackend.ClassLibrary.Profile.Request
{
    public class UpdateIndividualProfileRequest
    {
        public Guid? AccountId { get; set; }    
        public string? Id { get; set; }
        public byte[]? ProfilePhoto { get; set; }
        public byte[]? CoverPhoto { get; set; }
        public string? Bio { get; set; }
        public Guid? GenderId { get; set; }
        public string? Address { get; set; }
        public int? NeighborhoodId { get; set; }
    }
}
