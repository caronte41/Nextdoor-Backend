using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NextDoorBackend.SDK.Entities
{
    public class EventsParticipantsEntity
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("EventId")]
        public Guid EventId { get; set; }

        [ForeignKey("ProfileId")]
        public Guid ProfileId { get; set; }
        public ParticipationStatus Status { get; set; }
        public DateTime AttendedAt { get; set; } // Optional field to record when the user attended

        // Navigation properties
        public virtual EventsEntity Event { get; set; }
        public virtual ProfilesEntity Profile { get; set; }
    }
    public enum ParticipationStatus
    {
        Interested,
        Going
    }
}
