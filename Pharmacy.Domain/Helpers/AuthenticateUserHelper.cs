using System.Security.Claims;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.ModelsDb;

namespace Pharmacy.Domain.Helpers;

public class AuthenticateUserHelper
{
    public static ClaimsIdentity Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
            new Claim("AvatarPath", user.PartImage)
        };

        // Возвращаем объект ClaimsIdentity
        return new ClaimsIdentity(claims, "ApplicationCookie", ClaimTypes.Email, ClaimsIdentity.DefaultRoleClaimType);
    }
}