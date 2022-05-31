using System.Security.Claims;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface IJwtService
{
    public string GenerateAccessToken(List<Claim> claims);
}