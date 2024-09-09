using Mapster;
using Microsoft.EntityFrameworkCore;
using NextDoorBackend.ClassLibrary.MasterData.Response;
using NextDoorBackend.ClassLibrary.Profile.Request;
using NextDoorBackend.ClassLibrary.Profile.Response;
using NextDoorBackend.Data;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NextDoorBackend.Business.Profile
{
    public class ProfileInteractions : IProfileInteractions
    {
        private readonly AppDbContext _context;

        public ProfileInteractions(AppDbContext context)
        {
            _context = context;        
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
    }
}
