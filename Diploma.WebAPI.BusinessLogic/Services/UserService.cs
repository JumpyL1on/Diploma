using System.Security.Claims;
using Diploma.Common.Helpers;
using Diploma.Common.Requests;
using Diploma.WebAPI.BusinessLogic.Interfaces;
using Diploma.WebAPI.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace Diploma.WebAPI.BusinessLogic.Services;

public class UserService : IUserService
{
    public UserService(UserManager<User> userManager, IJWTService jwtService)
    {
        UserManager = userManager;
        JWTService = jwtService;
    }

    private UserManager<User> UserManager { get; }
    private IJWTService JWTService { get; }

    public async Task<Result<object>> SignUpAsync(SignUpUserRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            UserName = request.UserName,
            Surname = request.Surname,
            Email = request.Email
        };
        
        var result = await UserManager.CreateAsync(user, request.Password);
        
        if (result.Succeeded)
        {
            return new CreatedResult<object>();
        }

        var errors = result.Errors
            .Select(identityError => identityError.Description)
            .ToList();
        
        return new UnprocessableEntityResult<object>(errors);
    }

    public async Task<Result<string>> SignInAsync(SignInUserRequest request)
    {
        var user = await UserManager.FindByEmailAsync(request.Email);
        
        if (!await UserManager.CheckPasswordAsync(user, request.Password))
        {
            return new UnprocessableEntityResult<string>("Неверное имя пользователя или пароль");
        }

        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString())
        };
        
        return new OkResult<string>(JWTService.GenerateToken(claims));
    }
}