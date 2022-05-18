using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class JWTService : IJWTService
{
    private readonly JWTSettings _jwtSettings;

    public JWTService(IConfiguration configuration)
    {
        _jwtSettings = configuration
            .GetSection("JWTSettings")
            .Get<JWTSettings>();
    }

    public string GenerateToken(List<Claim> claims)
    {
        var bytes = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey);

        var key = new SymmetricSecurityKey(bytes);

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtSettings.ValidIssuer,
            _jwtSettings.ValidAudience,
            claims,
            null,
            DateTime.Now.AddMinutes(_jwtSettings.ExpiryInMinutes),
            credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}