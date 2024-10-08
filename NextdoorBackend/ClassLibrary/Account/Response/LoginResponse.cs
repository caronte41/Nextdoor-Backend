using NextDoorBackend.ClassLibrary.Profile.Request;
using NextDoorBackend.ClassLibrary.Profile.Response;

namespace NextDoorBackend.ClassLibrary.Account.Response
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public Guid AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<ProfileResponseForLogin> Profiles { get; set; }

    }
}
