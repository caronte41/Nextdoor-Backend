using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NextDoorBackend.SDK.Entities
{
    public class FavoritesEntitys
    {
        [Key]
        public Guid? Id { get; set; }

        [ForeignKey("ProfileId")]
        public Guid? ProfileId { get; set; }

        [ForeignKey("BusinessProfileId")]
        public Guid? BusinessProfileId { get; set; }
        public DateTime? FavoritedAt { get; set; }
        public virtual ProfilesEntity Profile { get; set; }
        public virtual BusinessProfilesEntity BusinessProfile { get; set; }
    }
}
