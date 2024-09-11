using Microsoft.AspNetCore.Mvc;

namespace NextDoorBackend.ClassLibrary.Common
{
    public class BaseRequest
    {
        [FromHeader(Name = "profile-id")]
        public Guid? profileId { get; set; }
    }
}
