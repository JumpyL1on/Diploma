using Diploma.Common.Helpers;
using Diploma.Common.Requests;

namespace Diploma.WebAPI.BusinessLogic.Interfaces;

public interface IUserService
{
    public Task<Result<object>> SignUpAsync(SignUpUserRequest request);
    public Task<Result<string>> SignInAsync(SignInUserRequest request);
}