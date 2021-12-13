using System.Threading.Tasks;
using Backend.Application.Features.RefreshJWT;
using Backend.WebAPI.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebAPI.Controllers
{
    [Route("api/jwt")]
    public class JWTController : BaseApiController
    {
        public JWTController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post(RefreshJWTCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}