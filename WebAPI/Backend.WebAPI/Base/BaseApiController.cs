using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebAPI.Base
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}