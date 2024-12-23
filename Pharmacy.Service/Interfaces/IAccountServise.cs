using System.Security.Claims;
using Pharmacy.Domain.Response;
using Pharmacy.Domain.Models;

namespace Pharmacy.Service.Interfaces;

public interface IAccountServise
{
    Task<BaseResponse<string>> Register(User model);
    Task<BaseResponse<ClaimsIdentity>> Login(User model);
    Task<BaseResponse<ClaimsIdentity>>ConfirmEmail(User model, string code,string confirmationCode);
    Task<BaseResponse<ClaimsIdentity>> IsCreatedAccount(User model);
    User GetUserByEmail(string email);
}