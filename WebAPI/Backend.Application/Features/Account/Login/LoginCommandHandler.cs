using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Interfaces;
using Backend.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Backend.Application.Features.Account.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private UserManager<AppUser> UserManager { get; }
    private IJWTService JWTService { get; }

    public LoginCommandHandler(UserManager<AppUser> userManager, IJWTService jwtService)
    {
        UserManager = userManager;
        JWTService = jwtService;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await UserManager.FindByEmailAsync(request.Email);
        if (!await UserManager.CheckPasswordAsync(user, request.Password))
            return new LoginResult
            {
                Succeeded = false,
                Error = "Неверное имя пользователя или пароль"
            };
        user.RefreshToken = JWTService.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await UserManager.UpdateAsync(user);
        var claims = await UserManager.GetClaimsAsync(user);
        claims.Add(new Claim("Id", user.Id.ToString()));
        return new LoginResult
        {
            Succeeded = true,
            Token = JWTService.CreateToken(claims),
            RefreshToken = user.RefreshToken
        };
    }
}