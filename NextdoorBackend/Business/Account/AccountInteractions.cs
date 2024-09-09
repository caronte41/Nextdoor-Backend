using NextDoorBackend.Business.GoogleMaps;
using NextDoorBackend.ClassLibrary.Account.Request;
using NextDoorBackend.ClassLibrary.Account.Response;
using NextDoorBackend.ClassLibrary.GoogleMaps.Request;
using NextDoorBackend.Data;
using NextDoorBackend.SDK.Entities;
using System.Security.Principal;

namespace NextDoorBackend.Business.Account
{
    public class AccountInteractions : IAccountInteractions
    {
        private readonly AppDbContext _context;
        private readonly IGoogleMapsInteractions _googleMapsInteractions;

        public AccountInteractions(AppDbContext context, IGoogleMapsInteractions googleMapsInteractions) 
        {
            _context = context;
            _googleMapsInteractions = googleMapsInteractions;
        }

        public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request)
        {
            var newAccountEntity = new AccountsEntity
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password
            };

            _context.Accounts.Add(newAccountEntity);
            await _context.SaveChangesAsync();

            var newProfile = new ProfilesEntity
            {
                Id = Guid.NewGuid(),
                AccountId = newAccountEntity.Id,
                ProfileName = $"{newAccountEntity.FirstName} {newAccountEntity.LastName}",
                ProfileType = "0" 
            };

            _context.Profiles.Add(newProfile);
            await _context.SaveChangesAsync();

            var latLngResponse = await _googleMapsInteractions.GetLatLngByAddress(new GetLanLngByAddressRequest { Address = request.Address });
            var neighborhoodId = await _googleMapsInteractions.GetNeighborhoodIdByLatLng(request.Latitude, request.Longitude);

            var newIndividualProfile = new IndividualProfilesEntity
            {
                Id = newProfile.Id,
              
                Address = request.Address,
                NeighborhoodId = neighborhoodId
            };

            _context.IndividualProfiles.Add(newIndividualProfile);
            await _context.SaveChangesAsync();

            return new CreateAccountResponse
            {
                AccountId = newAccountEntity.Id,
                ProfileId = newProfile.Id,
                IndividualProfileId = newProfile.Id
            };
        }

    }
}
