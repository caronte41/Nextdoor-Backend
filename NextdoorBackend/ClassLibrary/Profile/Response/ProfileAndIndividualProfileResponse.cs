using NextDoorBackend.ClassLibrary.Profile.Request;

namespace NextDoorBackend.ClassLibrary.Profile.Response
{
    public class ProfileAndIndividualProfileResponse
    {
        public UpsertBusinessProfileRequest BusinessProfile { get; set; }
        public GetIndividualProfileByAccountIdResponse IndividualProfile { get; set; }
    }
}
