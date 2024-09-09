using Microsoft.EntityFrameworkCore;
using NextDoorBackend.ClassLibrary.MasterData.Response;
using NextDoorBackend.ClassLibrary.Profile.Request;
using NextDoorBackend.ClassLibrary.Profile.Response;
using NextDoorBackend.Data;
using System.Net.Http;

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
    }
}
