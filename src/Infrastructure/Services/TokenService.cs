using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class TokenService(IConfiguration config, UserManager<User> userManager) : ITokenService
{
    public async Task<string> GetTokenAsync(User user)
    {
        var creds = GetCredentials();

        if (user.UserName is null || user.Email is null) throw new Exception("Can't access user.");

        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, user.Id),
            new("FirstName", user.FirstName),
            new("LastName", user.LastName),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email)
        ];

        var roles = await userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"],
            SigningCredentials = creds,
            Expires = DateTime.UtcNow.AddDays(1)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private SigningCredentials GetCredentials()
    {
        var tokenKey = config["Jwt:TokenKey"] ?? throw new Exception("Can't access TokenKey.");

        if (tokenKey.Length < 64) throw new Exception("TokenKey needs to be longer.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        return new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    }
}
