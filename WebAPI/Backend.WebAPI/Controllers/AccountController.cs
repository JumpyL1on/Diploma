using System.Threading.Tasks;
using Backend.Application.Features.Account.Login;
using Backend.Application.Features.Account.Register;
using Backend.WebAPI.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebAPI.Controllers
{
    [Route("api/account")]
    public class AccountController : BaseApiController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            return Ok(await Mediator.Send(registerCommand));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            return Ok(await Mediator.Send(loginCommand));
        }
    }
}