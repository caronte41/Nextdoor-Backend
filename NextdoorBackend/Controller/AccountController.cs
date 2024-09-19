﻿using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextDoorBackend.Business.Account;
using NextDoorBackend.Business.MasterData;
using NextDoorBackend.Business.SystemJob;
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
        private readonly IJwtService _jwtService;

        public AccountController(IAccountInteractions accountInteractions, AppDbContext context, IJwtService jwtService)
        {
            _accountInteractions = accountInteractions;
            _context = context;
            _jwtService = jwtService;
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
        [HttpGet("VerifyAccount")]
        public async Task<BaseResponseDto<CreateAccountResponse>> VerifyAccount([FromQuery] string token)
        {
            try
            {
                var response = await _accountInteractions.VerifyAccount(token);
                return BaseResponseDto<CreateAccountResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return BaseResponseDto<CreateAccountResponse>.Fail($"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("Login")]
        public async Task<BaseResponseDto<LoginResponse>> Login(ClassLibrary.Account.Request.LoginRequest request)
        {
            var user = await _context.Accounts
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null)
            {
                return BaseResponseDto<LoginResponse>.Fail("Invalid credentials");
            }

            if ((bool)!user.IsVerified)
            {
                return BaseResponseDto<LoginResponse>.Fail("Account is not verified");
            }

            var token = _jwtService.GenerateToken(user);

            return BaseResponseDto<LoginResponse>.Success(new LoginResponse
            {
                Token = token,
                AccountId = (Guid)user.Id
            });
        }
    }
}
