using System.ComponentModel.DataAnnotations.Schema;

namespace NextDoorBackend.SDK.Entities
{
    public class BusinessProfilesEntity
    {
        public Guid? Id { get; set; } // Foreign key from ProfilesEntity

        public string? LogoPhoto { get; set; }
        public string? CoverPhoto { get; set; }
        public string? BusinessName { get; set; }
        public string? Address { get; set; }

        [ForeignKey("NeighborhoodId")]
        public int? NeighborhoodId { get; set; }
        public virtual NeighborhoodEntity Neighborhood { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }

        public string? BusinessHours { get; set; } // JSON formatted string

        public string? BusinessStatus { get; set; }
        [ForeignKey("CategoryId")]
        public Guid[]? CategoryId { get; set; }
        public ICollection<BusinessCategoriesEntity> Categories { get; set; }
        public string? About { get; set; }

        public virtual ProfilesEntity Profile { get; set; }
        public virtual ICollection<FavoritesEntitys> Favorites { get; set; }
    }
}
