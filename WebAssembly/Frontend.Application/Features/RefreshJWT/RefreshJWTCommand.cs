using MediatR;

namespace Frontend.Application.Features.RefreshJWT
{
    public class RefreshJWTCommand : IRequest<Unit>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}