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
        //[HttpGet("GetEmployeeById")]
        //public async Task<BaseResponseDto<GetEmployeeByIdResponse>> GetEmployeeById([FromQuery] GetEmployeeByIdRequest requestDto)
        //{
        //    try
        //    {
        //        var response = await _employeeInteractions.GetEmployeeById(requestDto);
        //        return BaseResponseDto<GetEmployeeByIdResponse>.Success(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BaseResponseDto<GetEmployeeByIdResponse>.Fail($"Internal server error: {ex.Message}");
        //    }
        //}
    }
}
