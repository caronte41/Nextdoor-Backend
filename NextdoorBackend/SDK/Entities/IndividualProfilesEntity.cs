using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NextDoorBackend.SDK.Entities
{
    public class IndividualProfilesEntity
    {
        [Key, ForeignKey("Profile")]
        public Guid? Id { get; set; }

        public string? ProfilePhoto { get; set; }
        public string? CoverPhoto { get; set; }
        public string? Bio { get; set; }
        public Guid? GenderId { get; set; }

        public string? Address { get; set; }

        public int? NeighborhoodId { get; set; }
        [ForeignKey("NeighborhoodId")]
        public virtual NeighborhoodEntity Neighborhood { get; set; }

        [ForeignKey("GenderId")]
        public virtual GendersEntity Gender { get; set; }
        public virtual ProfilesEntity Profile { get; set; }
    }
}
