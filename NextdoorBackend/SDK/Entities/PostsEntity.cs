using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NextDoorBackend.SDK.Entities
{
    public class PostsEntity
    {
        [Key]
        public Guid? Id { get; set; }

        [ForeignKey("ProfileId")]
        public Guid? ProfileId { get; set; }
        public virtual ProfilesEntity Profile { get; set; }

        [Column(TypeName = "text")]
        public string? Summary { get; set; }

        [Column(TypeName = "text")]
        public string? PhotoUrl { get; set; }

        [Column(TypeName = "text")]
        public string? VideoUrl { get; set; }

        public DateTime? CreatedAt { get; set; } 

        [ForeignKey("NeighborhoodsEntity")]
        public int? NeighborhoodId { get; set; }
        public virtual NeighborhoodEntity Neighborhood { get; set; }
        public virtual ICollection<PostLikesEntity> Likes { get; set; }
        public virtual ICollection<PostCommentsEntity> Comments { get; set; }
    }
}
