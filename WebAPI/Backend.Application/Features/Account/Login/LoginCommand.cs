using MediatR;

namespace Backend.Application.Features.Account.Login
{
    public class LoginCommand : IRequest<LoginResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}