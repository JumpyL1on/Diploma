using Diploma.Common.Requests;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface IUserGameService
{
    public Task CreateAsync(CreateUserGameRequest request, Guid userId);
}