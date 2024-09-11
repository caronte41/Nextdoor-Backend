using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NextDoorBackend.SDK.Entities
{
    public class PostCommentsEntity
    {
        [Key]
        public Guid? Id { get; set; }

        [ForeignKey("ProfileId")]
        public Guid? ProfileId { get; set; }

        [ForeignKey("PostId")]
        public Guid? PostId { get; set; }

        public DateTime? CommentedAt { get; set; }

        public string? Comment { get; set; }

        // Navigation properties
        public virtual ProfilesEntity Profile { get; set; }
        public virtual PostsEntity Post { get; set; }
    }
}
