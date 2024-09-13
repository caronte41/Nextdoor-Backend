using NextDoorBackend.SDK.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextDoorBackend.ClassLibrary.Notification.Response
{
    public class GetNotificationsByAccountIdResponse
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public NotificationType? Type { get; set; }
        public string? Message { get; set; }
        public bool? IsRead { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public Guid? RelatedEntityId { get; set; }
    }
}
