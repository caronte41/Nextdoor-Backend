using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NextDoorBackend.SDK.Entities
{
    public class NotificationsEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountsEntity Account { get; set; }
        public NotificationType? Type { get; set; }
        public string? Message { get; set; }

        public bool? IsRead { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public Guid? RelatedEntityId { get; set; } // e.g., FriendshipConnectionId for friendship request
    }

    public enum NotificationType
    {
        FriendshipRequest,
        FriendshipResponse,
        // Other types of notifications can be added here, e.g., Comment, Like, etc.
    }
}
