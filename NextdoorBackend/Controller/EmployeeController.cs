using Microsoft.AspNetCore.Mvc;
using NextDoorBackend.Data;
using NextDoorBackend.SDK.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using NextDoorBackend.Business.Employee;
using NextDoorBackend.ClassLibrary.Employee.Request;
using NextDoorBackend.ClassLibrary.Employee.Response;
using NextDoorBackend.ClassLibrary.Common;

namespace NextDoorBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeInteractions _employeeInteractions;

        public EmployeeController(IEmployeeInteractions employeeInteractions, AppDbContext context)
        {
            _employeeInteractions = employeeInteractions;
            _context = context;
        }

        [HttpPost("UpsertEmployee")]
        public async Task<BaseResponseDto<UpsertEmployeeResponse>> UpsertEmployee([FromBody] UpsertEmployeeRequest requestDto)
        {
            try
            {
                    var response = await _employeeInteractions.UpsertEmployee(requestDto);
                    return BaseResponseDto<UpsertEmployeeResponse>.Success(response);  
            }
            catch (Exception ex)
            {
                return BaseResponseDto<UpsertEmployeeResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetEmployeeById")]
        public async Task<BaseResponseDto<GetEmployeeByIdResponse>> GetEmployeeById([FromQuery] GetEmployeeByIdRequest requestDto)
        {
            try
            {
                var response = await _employeeInteractions.GetEmployeeById(requestDto);
                return BaseResponseDto<GetEmployeeByIdResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<GetEmployeeByIdResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
    }
}
