using MediatR;

namespace Backend.Application.Features.RefreshJWT;

public class RefreshJWTCommand : IRequest<RefreshJWTResponse>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}