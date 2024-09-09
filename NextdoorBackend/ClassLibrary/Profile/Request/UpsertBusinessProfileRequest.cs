using NextDoorBackend.ClassLibrary.GoogleMaps.Response;
using NextDoorBackend.SDK.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextDoorBackend.ClassLibrary.Profile.Request
{
    public class UpsertBusinessProfileRequest : GetLanLngByAddressResponse
    {
        public Guid? Id { get; set; } 
        public Guid? AccountId { get; set; }
        public string? LogoPhoto { get; set; }
        public string? CoverPhoto { get; set; }
        public string? BusinessName { get; set; }
        public string? Address { get; set; }
        public int? NeighborhoodId { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? BusinessHours { get; set; } 
        public string? BusinessStatus { get; set; }
        public Guid[]? CategoryId { get; set; }
        public string? About { get; set; }
    }
}
