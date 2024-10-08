using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NextDoorBackend.Business.GoogleMaps;
using NextDoorBackend.Business.MasterData;
using NextDoorBackend.ClassLibrary.Common;
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
        private readonly IMasterDataInteractions _masterDataInteractions;
        public ProfileInteractions(AppDbContext context, IGoogleMapsInteractions googleMapsInteractions, IMasterDataInteractions masterDataInteractions)
        {
            _context = context;
            _googleMapsInteractions = googleMapsInteractions;
            _masterDataInteractions = masterDataInteractions;
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
                    individualProfileEntity.ProfilePhoto = request.ProfilePhoto != null ? await _masterDataInteractions.SaveFile(request.ProfilePhoto, "photos") : individualProfileEntity.ProfilePhoto;
                    individualProfileEntity.CoverPhoto = request.CoverPhoto != null ? await _masterDataInteractions.SaveFile(request.CoverPhoto, "photos") : individualProfileEntity.CoverPhoto;
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
               .ThenInclude(p => p.BusinessProfile) 
               .FirstOrDefaultAsync(a => a.Id == request.AccountId && a.Profiles.Any(p => p.ProfileType == "1"));

            var neighborhoodId = request.Latitude ==0 ? await _googleMapsInteractions.GetNeighborhoodIdByLatLng(request.Latitude, request.Longitude):501;

            if (accountEntity != null)
            {
                var businessProfileEntity = accountEntity.Profiles
                    .FirstOrDefault(p => p.ProfileType == "1")?.BusinessProfile;

                if (businessProfileEntity != null)
                {
                    // Update the business profile entity
                   
                    businessProfileEntity.LogoPhoto = request.LogoPhoto != null ? await _masterDataInteractions.SaveFile(request.LogoPhoto, "photos") : businessProfileEntity.LogoPhoto;
                    businessProfileEntity.CoverPhoto = request.CoverPhoto != null ? await _masterDataInteractions.SaveFile(request.CoverPhoto, "photos") : businessProfileEntity.CoverPhoto;
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

        public async Task<BusinessProfileResponse> GetBusinessProfileByAccountId(GetIndividualProfileByAccountIdRequest request)
        {
    
            var account = await _context.Accounts
                .Include(a => a.Profiles)
                .ThenInclude(p => p.BusinessProfile)
                .FirstOrDefaultAsync(a => a.Id == request.AccountId && a.Profiles.Any(p => p.ProfileType == "1"));

            var businessProfile = account.Profiles
                .FirstOrDefault(p => p.ProfileType == "1")?
                .BusinessProfile;

            if (businessProfile != null)
            {

                var categoryNames = await _context.BusinessCategories
                    .Where(c => businessProfile.CategoryId != null && businessProfile.CategoryId.Any(id => id == c.Id))
                    .Select(c => c.CategoryName)
                    .ToListAsync();

                var response = businessProfile.Adapt<BusinessProfileResponse>();

                response.CategoryNames = categoryNames; 

                return response;
            }

            return null; 
        }

        public async Task<List<UpsertBusinessProfileRequest>> GetAllBusinessProfiles(BaseRequest request)
        {
            var data = await _context.BusinessProfiles.ToListAsync();

            // Get the list of business IDs that the current user has favorited
            var favoritedBusinessIds = await _context.Favorites
                .Where(f => f.ProfileId == request.profileId)
                .Select(f => f.BusinessProfileId)
                .ToListAsync();

            // Map and set the UserAddedToFavorite flag
            var response = data.Select(business =>
            {
                var businessDto = business.Adapt<UpsertBusinessProfileRequest>();
                businessDto.UserAddedToFavorite = favoritedBusinessIds.Contains(business.Id);
                return businessDto;
            }).ToList();

            return response;
        }

    }
}
