using Microsoft.AspNetCore.Mvc;
using NextDoorBackend.Business.MasterData;
using NextDoorBackend.Business.Profile;
using NextDoorBackend.ClassLibrary.Common;
using NextDoorBackend.ClassLibrary.MasterData.Response;
using NextDoorBackend.ClassLibrary.Profile.Request;
using NextDoorBackend.ClassLibrary.Profile.Response;
using NextDoorBackend.Data;

namespace NextDoorBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IProfileInteractions _profileInteractions;

        public ProfileController(IProfileInteractions profileInteractions, AppDbContext context)
        {
            _profileInteractions = profileInteractions;
            _context = context;
        }
        [HttpPost("UpdateIndividualProfile")]
        public async Task<BaseResponseDto<UpdateIndividualProfileResponse>> UpdateIndividualProfile(UpdateIndividualProfileRequest requestDto)
        {
            try
            {
                var response = await _profileInteractions.UpdateIndividualProfile(requestDto);
                return BaseResponseDto<UpdateIndividualProfileResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<UpdateIndividualProfileResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetIndividualProfileByAccountId")]
        public async Task<BaseResponseDto<GetIndividualProfileByAccountIdResponse>> GetIndividualProfileByAccountId([FromQuery] GetIndividualProfileByAccountIdRequest requestDto)
        {
            try
            {
                var response = await _profileInteractions.GetIndividualProfileByAccountId(requestDto);
                return BaseResponseDto<GetIndividualProfileByAccountIdResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<GetIndividualProfileByAccountIdResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("UpsertBusinessProfile")]
        public async Task<BaseResponseDto<UpsertBusinessProfileResponse>> UpsertBusinessProfile(UpsertBusinessProfileRequest requestDto)
        {
            try
            {
                var response = await _profileInteractions.UpsertBusinessProfile(requestDto);
                return BaseResponseDto<UpsertBusinessProfileResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<UpsertBusinessProfileResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
    }
}
