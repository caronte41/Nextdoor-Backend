namespace NextDoorBackend.ClassLibrary.Account.Request
{
    public class CreateAccountRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? ProfileType { get; set; }
        public string? ProfileName { get; set; }
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; } 
        public int? NeighborhoodId { get; set; }
    }
}
