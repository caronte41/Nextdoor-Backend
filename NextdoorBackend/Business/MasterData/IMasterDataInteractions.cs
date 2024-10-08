using NextDoorBackend.ClassLibrary.MasterData.Request;
using NextDoorBackend.ClassLibrary.MasterData.Response;

namespace NextDoorBackend.Business.MasterData
{
    public interface IMasterDataInteractions
    {
        Task<UpsertBusinessCategoryResponse> UpsertBusinessCategory(UpsertBusinessCategoriesRequest request);
        Task<UpsertGenderResponse> UpsertGender(UpsertGenderRequest request);
        Task<GetGenderByIdResponse> GetGenderById(GetGenderByIdRequest request);
        Task<GetBusinessCategoryByIdResponse> GetBusinessCategoryById(GetBusinessCategoryByIdRequest request);
        Task<List<GetGendersResponse>> GetAllGenders(GetGendersRequest request);
        Task<List<GetBusinessCategoriesResponse>> GetAllBusinessCategories(GetBusinessCategoriesRequest request);
        Task<string> SaveFile(byte[] fileData, string folderName);
       
    }
}
