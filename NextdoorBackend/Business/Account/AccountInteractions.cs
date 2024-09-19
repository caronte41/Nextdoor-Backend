
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IEmailService _emailService;

        public AccountInteractions(AppDbContext context, IGoogleMapsInteractions googleMapsInteractions, IEmailService emailService) 
        {
            _context = context;
            _googleMapsInteractions = googleMapsInteractions;
            _emailService = emailService;
        }

        public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request)
        {
            var existingAccount = await _context.Accounts
        .FirstOrDefaultAsync(a => a.Email == request.Email);

            if (existingAccount != null)
            {
                // Email already exists, return an error response
                throw new InvalidOperationException("An account with this email already exists.");
            }

            var newAccountEntity = new AccountsEntity
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password,
                VerificationToken = Guid.NewGuid().ToString()
            };

            _context.Accounts.Add(newAccountEntity);
      

            var verificationUrl = $"https://localhost:3000/verify?token={newAccountEntity.VerificationToken}";
            var emailBody = $"Hello {newAccountEntity.FirstName},<br>Please verify your account by clicking the link below:<br><a href='{verificationUrl}'>Verify Account</a>";

            await _emailService.SendEmailAsync(newAccountEntity.Email, "Verify your account", emailBody);

            var newProfile = new ProfilesEntity
            {
                Id = Guid.NewGuid(),
                AccountId = newAccountEntity.Id,
                ProfileName = $"{newAccountEntity.FirstName} {newAccountEntity.LastName}",
                ProfileType = "0" 
            };

            _context.Profiles.Add(newProfile);
       

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
        public async Task<CreateAccountResponse> VerifyAccount(string token)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.VerificationToken == token);

            account.IsVerified = true;
            await _context.SaveChangesAsync();

            return new CreateAccountResponse
            {
                AccountId = account.Id
               
            };
        }

    }
}
