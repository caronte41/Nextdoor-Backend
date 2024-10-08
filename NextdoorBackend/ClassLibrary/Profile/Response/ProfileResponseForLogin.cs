using NextDoorBackend.ClassLibrary.Profile.Request;

namespace NextDoorBackend.ClassLibrary.Profile.Response
{
    public class ProfileResponseForLogin
    {
        public Guid? Id { get; set; }
        public string? ProfileType { get; set; }
        public string? ProfileName { get; set; }
        public Guid? AccountId { get; set; }
        public BusinessProfileResponse BusinessProfile { get; set; }
        public GetIndividualProfileByAccountIdResponse IndividualProfile { get; set; }
    }
}
