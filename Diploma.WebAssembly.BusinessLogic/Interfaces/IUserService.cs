using Diploma.Common.Requests;

namespace Diploma.WebAssembly.BusinessLogic.Interfaces;

public interface IUserService
{
    public Task SignUpUserAsync(SignUpUserRequest request);
    public Task SignInUserAsync(SignInUserRequest request);
    public Task SignOutUserAsync();
}