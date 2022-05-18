using System.Security.Claims;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface IJWTService
{
    public string GenerateToken(List<Claim> claims);
}