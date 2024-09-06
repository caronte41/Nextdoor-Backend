using Microsoft.EntityFrameworkCore;

namespace NextDoorBackend.SDK.Entities
{
    public class ProfilesEntity
    {
        public Guid? Id { get; set; }
        public string? ProfileType { get; set; }
        public string? ProfileName { get; set; }

        // Foreign key to Account
        public Guid? AccountId { get; set; }
        public virtual AccountsEntity Account { get; set; }
        public virtual IndividualProfilesEntity IndividualProfile { get; set; }
        public virtual BusinessProfilesEntity BusinessProfile { get; set; }
    }
}
