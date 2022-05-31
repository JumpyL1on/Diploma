using System.Security.Claims;
using Diploma.Common.Exceptions;
using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class UserService : IUserService
{
    public UserService(UserManager<User> userManager, IJwtService jwtService)
    {
        UserManager = userManager;
        JWTService = jwtService;
    }

    private UserManager<User> UserManager { get; }
    private IJwtService JWTService { get; }

    public async Task SignUpUserAsync(SignUpUserRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            UserName = request.UserName,
            Surname = request.Surname,
            Email = request.Email
        };

        var result = await UserManager.CreateAsync(user, request.Password);

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
        var user = await UserManager.FindByEmailAsync(request.Email);

        if (!await UserManager.CheckPasswordAsync(user, request.Password))
        {
            throw new ValidationException("Неверное имя пользователя или пароль");
        }

        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new("Name", user.UserName)
        };

        return JWTService.GenerateAccessToken(claims);
    }
}