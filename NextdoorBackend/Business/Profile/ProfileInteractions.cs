using Mapster;
using Microsoft.EntityFrameworkCore;
using NextDoorBackend.Business.GoogleMaps;
using NextDoorBackend.ClassLibrary.MasterData.Response;
using NextDoorBackend.ClassLibrary.Profile.Request;
using NextDoorBackend.ClassLibrary.Profile.Response;
using NextDoorBackend.Data;
using NextDoorBackend.SDK.Entities;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NextDoorBackend.Business.Profile
{
    public class ProfileInteractions : IProfileInteractions
    {
        private readonly AppDbContext _context;
        private readonly IGoogleMapsInteractions _googleMapsInteractions;
        public ProfileInteractions(AppDbContext context, IGoogleMapsInteractions googleMapsInteractions)
        {
            _context = context;
            _googleMapsInteractions = googleMapsInteractions;
        }
        public async Task<UpdateIndividualProfileResponse> UpdateIndividualProfile(UpdateIndividualProfileRequest request)
        {
            var accountEntity = await _context.Accounts
        .Include(a => a.Profiles)
        .ThenInclude(p => p.IndividualProfile)
        .FirstOrDefaultAsync(a => a.Id == request.AccountId && a.Profiles.Any(p => p.ProfileType == "0"));


            if (accountEntity != null)
            {
                var individualProfileEntity = accountEntity.Profiles
            .FirstOrDefault(p => p.ProfileType == "0")?.IndividualProfile;

                if (individualProfileEntity != null)
                {
                    // Update the individual profile entity
                    individualProfileEntity.ProfilePhoto = request.ProfilePhoto;
                    individualProfileEntity.CoverPhoto = request.CoverPhoto;
                    individualProfileEntity.Bio = request.Bio;
                    individualProfileEntity.GenderId = request.GenderId;
                    individualProfileEntity.Address = request.Address;
                    individualProfileEntity.NeighborhoodId = request.NeighborhoodId;

                    _context.IndividualProfiles.Update(individualProfileEntity);
                    await _context.SaveChangesAsync();

                    return new UpdateIndividualProfileResponse
                    {
                        Id = individualProfileEntity.Id
                    };
                }
            }

            // Return null or handle the case where the profile wasn't found
            return null;
        }
        public async Task<GetIndividualProfileByAccountIdResponse> GetIndividualProfileByAccountId(GetIndividualProfileByAccountIdRequest request)
        {
            var account = await _context.Accounts
        .Include(a => a.Profiles)
        .ThenInclude(p => p.IndividualProfile)
        .FirstOrDefaultAsync(a => a.Id == request.AccountId && a.Profiles.Any(p => p.ProfileType == "0"));

            var individualProfile = account.Profiles
     .FirstOrDefault(p => p.ProfileType == "0")?
     .IndividualProfile;

            var response = individualProfile.Adapt<GetIndividualProfileByAccountIdResponse>();
            return response;
        }
        public async Task<UpsertBusinessProfileResponse> UpsertBusinessProfile(UpsertBusinessProfileRequest request)
        {
            var accountEntity = await _context.Accounts
               .Include(a => a.Profiles)
               .ThenInclude(p => p.BusinessProfile) // Changed to BusinessProfile instead of IndividualProfile
               .FirstOrDefaultAsync(a => a.Id == request.AccountId && a.Profiles.Any(p => p.ProfileType == "1"));

            var neighborhoodId = await _googleMapsInteractions.GetNeighborhoodIdByLatLng(request.Latitude, request.Longitude);

            if (accountEntity != null)
            {
                var businessProfileEntity = accountEntity.Profiles
                    .FirstOrDefault(p => p.ProfileType == "1")?.BusinessProfile;

                if (businessProfileEntity != null)
                {
                    // Update the business profile entity
                    businessProfileEntity.LogoPhoto = request.LogoPhoto;
                    businessProfileEntity.CoverPhoto = request.CoverPhoto;
                    businessProfileEntity.BusinessName = request.BusinessName;
                    businessProfileEntity.Address = request.Address;
                    businessProfileEntity.Email = request.Email;
                    businessProfileEntity.NeighborhoodId = request.NeighborhoodId;
                    businessProfileEntity.Website = request.Website;
                    businessProfileEntity.Phone = request.Phone;
                    businessProfileEntity.NeighborhoodId = neighborhoodId;
                    businessProfileEntity.Latitude = request.Latitude;
                    businessProfileEntity.Longitude = request.Longitude;
                    businessProfileEntity.BusinessHours = request.BusinessHours;
                    businessProfileEntity.BusinessStatus = request.BusinessStatus;
                    businessProfileEntity.CategoryId = request.CategoryId;
                    businessProfileEntity.About = request.About;

                    _context.BusinessProfiles.Update(businessProfileEntity);
                    await _context.SaveChangesAsync();

                    return new UpsertBusinessProfileResponse
                    {
                        Id = businessProfileEntity.Id
                    };
                }
            }

            // Create a new profile and business profile if one does not exist
            var newProfile = new ProfilesEntity
            {
                Id = Guid.NewGuid(),
                AccountId = request.AccountId,
                ProfileName = request.BusinessName,
                ProfileType = "1"
            };

            _context.Profiles.Add(newProfile);
            await _context.SaveChangesAsync();

            var newBusinessProfileEntity = new BusinessProfilesEntity
            {
                Id = newProfile.Id,
                LogoPhoto = request.LogoPhoto,
                CoverPhoto = request.CoverPhoto,
                BusinessName = request.BusinessName,
                Address = request.Address,
                Email = request.Email,
              NeighborhoodId = neighborhoodId,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Website = request.Website,
                Phone = request.Phone,
                BusinessHours = request.BusinessHours,
                BusinessStatus = request.BusinessStatus,
                CategoryId = request.CategoryId,
                About = request.About
            };

            _context.BusinessProfiles.Add(newBusinessProfileEntity);
            await _context.SaveChangesAsync();

            return new UpsertBusinessProfileResponse
            {
                Id = newBusinessProfileEntity.Id
            };
        }



    }
}
