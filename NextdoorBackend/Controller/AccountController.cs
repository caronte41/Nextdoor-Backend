using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextDoorBackend.Business.Account;
using NextDoorBackend.Business.MasterData;
using NextDoorBackend.ClassLibrary.Account.Request;
using NextDoorBackend.ClassLibrary.Account.Response;
using NextDoorBackend.ClassLibrary.Common;
using NextDoorBackend.ClassLibrary.MasterData.Request;
using NextDoorBackend.ClassLibrary.MasterData.Response;
using NextDoorBackend.Data;

namespace NextDoorBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAccountInteractions _accountInteractions;

        public AccountController(IAccountInteractions accountInteractions, AppDbContext context)
        {
            _accountInteractions = accountInteractions;
            _context = context;
        }
        [HttpPost("CreateAccount")]
        public async Task<BaseResponseDto<CreateAccountResponse>> CreateAccount([FromBody] CreateAccountRequest requestDto)
        {
            try
            {
                var response = await _accountInteractions.CreateAccount(requestDto);
                return BaseResponseDto<CreateAccountResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<CreateAccountResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
    }
}
