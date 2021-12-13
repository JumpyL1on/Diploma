using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Frontend.Application.Features.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}