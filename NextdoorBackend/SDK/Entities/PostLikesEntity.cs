using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NextDoorBackend.SDK.Entities
{
    public class PostLikesEntity
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ProfileId")]
        public Guid ProfileId { get; set; }

        [ForeignKey("PostId")]
        public Guid PostId { get; set; }

        public DateTime LikedAt { get; set; }

        // Navigation properties
        public ProfilesEntity Profile { get; set; }
        public PostsEntity Post { get; set; }
    }
}
