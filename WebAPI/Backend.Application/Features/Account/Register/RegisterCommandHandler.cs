using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Backend.Application.Features.Account.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResult>
    {
        private UserManager<AppUser> UserManager { get; }

        public RegisterCommandHandler(UserManager<AppUser> userManager)
        {
            UserManager = userManager;
        }

        public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                Name = request.Name,
                UserName = request.UserName,
                Surname = request.Surname,
                Email = request.Email
            };
            var result = await UserManager.CreateAsync(user, request.Password);
            return new RegisterResult
            {
                Succeeded = result.Succeeded,
                Errors = result.Errors.Select(error => error.Description).ToList()
            };
        }
    }
}