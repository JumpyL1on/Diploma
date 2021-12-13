using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Frontend.Application.Features.Register
{
    public class RegisterCommand : IRequest<RegisterResponse>
    {
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}