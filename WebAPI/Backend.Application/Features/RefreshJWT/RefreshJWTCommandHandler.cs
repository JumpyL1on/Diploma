using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Interfaces;
using Backend.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Backend.Application.Features.RefreshJWT;

public class RefreshJWTCommandHandler : IRequestHandler<RefreshJWTCommand, RefreshJWTResponse>
{
    private UserManager<AppUser> UserManager { get; }
    private IJWTService JWTService { get; }

    public RefreshJWTCommandHandler(UserManager<AppUser> userManager, IJWTService jwtService)
    {
        UserManager = userManager;
        JWTService = jwtService;
    }

    public async Task<RefreshJWTResponse> Handle(RefreshJWTCommand request, CancellationToken cancellationToken)
    {
        var principal = JWTService.GetPrincipalFromExpiredToken(request.Token);
        var user = await UserManager.FindByIdAsync(principal?.FindFirst("Id")?.Value);
        if (user == null || user.RefreshToken != request.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now)
            return new RefreshJWTResponse { Succeeded = false };
        var claims = await UserManager.GetClaimsAsync(user);
        claims.Add(new Claim("Id", user.Id.ToString()));
        var token = JWTService.CreateToken(claims);
        user.RefreshToken = JWTService.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await UserManager.UpdateAsync(user);
        return new RefreshJWTResponse { Succeeded = true, Token = token, RefreshToken = user.RefreshToken };
    }
}