using System.ComponentModel.DataAnnotations.Schema;

namespace NextDoorBackend.ClassLibrary.Post.Request
{
    public class AddPostLikeRequest
    {
        public Guid? Id { get; set; }
        public Guid? ProfileId { get; set; }
        public Guid? PostId { get; set; }
        public DateTime? LikedAt { get; set; }
    }
}
