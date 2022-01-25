using Backend.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Backend.WebAPI.Base;

[Authorize]
public class AuthorizedApiController : BaseApiController
{
    protected UserManager<AppUser> UserManager { get; }

    public AuthorizedApiController(UserManager<AppUser> userManager, IMediator mediator) : base(mediator)
    {
        UserManager = userManager;
    }
}