using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApiBoilerplate.Controllers
{
    [Route("api/")]
    [ApiController]
#pragma warning disable CS8618, CS8603, CS8601
    public class ApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
#pragma warning restore CS8618, CS8603, CS8601
}
