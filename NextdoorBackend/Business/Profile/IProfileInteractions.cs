using NextDoorBackend.ClassLibrary.Profile.Request;
using NextDoorBackend.ClassLibrary.Profile.Response;

namespace NextDoorBackend.Business.Profile
{
    public interface IProfileInteractions
    {
        Task<UpdateIndividualProfileResponse> UpdateIndividualProfile(UpdateIndividualProfileRequest request);
        Task<GetIndividualProfileByAccountIdResponse> GetIndividualProfileByAccountId(GetIndividualProfileByAccountIdRequest request);
        Task<UpsertBusinessProfileResponse> UpsertBusinessProfile(UpsertBusinessProfileRequest request);
        Task<UpsertBusinessProfileRequest> GetBusinessProfileByAccountId(GetIndividualProfileByAccountIdRequest request);
        Task<List<UpsertBusinessProfileRequest>> GetAllBusinessProfiles();
    }
}
