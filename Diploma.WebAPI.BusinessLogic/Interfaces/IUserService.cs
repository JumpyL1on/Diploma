using Diploma.Common.Requests;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface IUserService
{
    public Task SignUpUserAsync(SignUpUserRequest request);
    public Task<string> SignInUserAsync(SignInUserRequest request);
}