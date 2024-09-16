using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NextDoorBackend.SDK.Entities
{
    public class EventsEntity
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ProfileId")]
        public Guid ProfileId { get; set; }

        public DateTime EventDay { get; set; }
        public TimeSpan EventHour { get; set; }
        public string EventName { get; set; }
        public string OrganizatorName { get; set; }
        public string Address { get; set; }

        [ForeignKey("NeighborhoodId")]
        public int NeighborhoodId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; } // Enum or integer for event status

        // Navigation properties
        public virtual ProfilesEntity Profile { get; set; }
        public virtual NeighborhoodEntity Neighborhood { get; set; }
        public virtual ICollection<EventsParticipantsEntity> EventsParticipants { get; set; }
  
    }
}
