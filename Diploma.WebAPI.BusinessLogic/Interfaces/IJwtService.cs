using Diploma.WebAPI.DataAccess.Entities;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface IJwtService
{
    public string GenerateAccessToken(User user);
}