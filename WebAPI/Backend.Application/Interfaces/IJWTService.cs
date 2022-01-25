using System.Collections.Generic;
using System.Security.Claims;

namespace Backend.Application.Interfaces;

public interface IJWTService
{
    string CreateToken(IList<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}