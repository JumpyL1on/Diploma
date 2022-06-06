using Diploma.Common.Exceptions;
using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;

    public UserService(UserManager<User> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task SignUpUserAsync(SignUpUserRequest request)
    {
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors
                .Select(identityError => identityError.Description)
                .ToList();

            throw new BusinessException(errors[0]);
        }
    }

    public async Task<string> SignInUserAsync(SignInUserRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new ValidationException("Неверное имя пользователя или пароль");
        }

        return _jwtService.GenerateAccessToken(user);
    }
}