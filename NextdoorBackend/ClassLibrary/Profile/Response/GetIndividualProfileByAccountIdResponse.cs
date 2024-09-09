namespace NextDoorBackend.ClassLibrary.Profile.Response
{
    public class GetIndividualProfileByAccountIdResponse
    {
        public Guid? AccountId { get; set; }
        public string? Id { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? CoverPhoto { get; set; }
        public string? Bio { get; set; }
        public Guid? GenderId { get; set; }
        public string? Address { get; set; }
        public int? NeighborhoodId { get; set; }
    }
}
