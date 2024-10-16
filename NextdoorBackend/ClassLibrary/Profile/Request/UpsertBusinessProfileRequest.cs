﻿using NextDoorBackend.ClassLibrary.GoogleMaps.Response;
using NextDoorBackend.SDK.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextDoorBackend.ClassLibrary.Profile.Request
{
    public class UpsertBusinessProfileRequest : GetLanLngByAddressResponse
    {
        public Guid? Id { get; set; } 
        public Guid? AccountId { get; set; }
        public byte[]? LogoPhoto { get; set; }
        public byte[]? CoverPhoto { get; set; }
        public string? BusinessName { get; set; }
        public string? Address { get; set; }
        public int? NeighborhoodId { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? BusinessHours { get; set; } 
        public string? BusinessStatus { get; set; }
        public Guid[]? CategoryId { get; set; }
        public List<string>? CategoryNames { get; set; }
        public string? About { get; set; }
        public bool? UserAddedToFavorite { get; set; }
    }
}
