using Microsoft.AspNetCore.Mvc;
using NextDoorBackend.ClassLibrary.Account.Request;
using NextDoorBackend.ClassLibrary.Account.Response;

namespace NextDoorBackend.Business.Account
{
    public interface IAccountInteractions
    {
        Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request);
        Task<CreateAccountResponse> VerifyAccount(string token);
    }
}
