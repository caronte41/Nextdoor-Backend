using NextDoorBackend.SDK.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourNamespace.Entities
{
    public class FriendshipConnectionsEntity
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid? RequestorAccountId { get; set; }

        [ForeignKey("RequestorAccountId")]
        public virtual AccountsEntity RequestorAccount { get; set; }

        [Required]
        public Guid? ReceiverAccountId { get; set; }

        [ForeignKey("ReceiverAccountId")]
        public virtual AccountsEntity ReceiverAccount { get; set; }

        [Required]
        public FriendshipStatus Status { get; set; }

        [Required]
        public DateTime? RequestedAt { get; set; }

        public DateTime? RespondedAt { get; set; }

        public DateTime? BlockedAt { get; set; }

        public bool? IsMutual { get; set; }
    }

    public enum FriendshipStatus
    {
        Pending,
        Accepted,
        Rejected,
        Blocked
    }
}
