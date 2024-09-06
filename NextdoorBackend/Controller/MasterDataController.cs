using Microsoft.AspNetCore.Mvc;
using NextDoorBackend.Data;
using NextDoorBackend.SDK.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using NextDoorBackend.Business.Employee;
using NextDoorBackend.ClassLibrary.Employee.Request;
using NextDoorBackend.ClassLibrary.Employee.Response;
using NextDoorBackend.ClassLibrary.Common;
using NextDoorBackend.Business.MasterData;
using NextDoorBackend.ClassLibrary.MasterData.Request;
using NextDoorBackend.ClassLibrary.MasterData.Response;

namespace NextDoorBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterDataController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMasterDataInteractions _masterDataInteractions;

        public MasterDataController(IMasterDataInteractions masterDataInteractions, AppDbContext context)
        {
            _masterDataInteractions = masterDataInteractions;
            _context = context;
        }

        [HttpPost("UpsertBusinessCategory")]
        public async Task<BaseResponseDto<UpsertBusinessCategoryResponse>> UpsertBusinessCategory([FromBody] UpsertBusinessCategoriesRequest requestDto)
        {
            try
            {
                var response = await _masterDataInteractions.UpsertBusinessCategory(requestDto);
                return BaseResponseDto<UpsertBusinessCategoryResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<UpsertBusinessCategoryResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("UpsertGender")]
        public async Task<BaseResponseDto<UpsertGenderResponse>> UpsertGender([FromBody] UpsertGenderRequest requestDto)
        {
            try
            {
                var response = await _masterDataInteractions.UpsertGender(requestDto);
                return BaseResponseDto<UpsertGenderResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<UpsertGenderResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllGenders")]
        public async Task<BaseResponseDto<List<GetGendersResponse>>> GetAllGenders([FromQuery]GetGendersRequest requestDto)
        {
            try
            {
                var response = await _masterDataInteractions.GetAllGenders(requestDto);
                return BaseResponseDto<List<GetGendersResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<List<GetGendersResponse>>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllBusinessCategories")]
        public async Task<BaseResponseDto<List<GetBusinessCategoriesResponse>>> GetAllBusinessCategories([FromQuery] GetBusinessCategoriesRequest requestDto)
        {
            try
            {
                var response = await _masterDataInteractions.GetAllBusinessCategories(requestDto);
                return BaseResponseDto<List<GetBusinessCategoriesResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<List<GetBusinessCategoriesResponse>>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetGenderById")]
        public async Task<BaseResponseDto<GetGenderByIdResponse>> GetGenderById([FromQuery] GetGenderByIdRequest requestDto)
        {
            try
            {
                var response = await _masterDataInteractions.GetGenderById(requestDto);
                return BaseResponseDto<GetGenderByIdResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<GetGenderByIdResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetBusinessCategoryById")]
        public async Task<BaseResponseDto<GetBusinessCategoryByIdResponse>> GetBusinessCategoryById([FromQuery] GetBusinessCategoryByIdRequest requestDto)
        {
            try
            {
                var response = await _masterDataInteractions.GetBusinessCategoryById(requestDto);
                return BaseResponseDto<GetBusinessCategoryByIdResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<GetBusinessCategoryByIdResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
       
    }
}
