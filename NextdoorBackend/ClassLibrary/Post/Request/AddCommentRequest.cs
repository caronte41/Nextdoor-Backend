using System.ComponentModel.DataAnnotations.Schema;

namespace NextDoorBackend.ClassLibrary.Post.Request
{
    public class AddCommentRequest
    {
        public Guid? Id { get; set; }
        public Guid? ProfileId { get; set; }
        public Guid? PostId { get; set; }
        public DateTime? CommentedAt { get; set; }
        public string? Comment { get; set; }
    }
}
