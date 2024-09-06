using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NextDoorBackend.SDK.Entities
{
    public class AccountsEntity
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }

        // Navigation property for related profiles
        public virtual ICollection<ProfilesEntity> Profiles { get; set; }
    }
}
