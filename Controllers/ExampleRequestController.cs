using NetCoreApiBoilerplate.Mediators.Samples;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreApiBoilerplate.Middlewares.CustomAuthorization;

namespace DotnetApiBoilerplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleRequestController : ApiController
    {
        [HttpGet("get-example")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ExampleRequestResult> GetSampleRequest([FromQuery] ExampleRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpGet("authentication-test")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AuthenticationTest([FromQuery] ExampleRequest request)
        {
            var res = await Mediator.Send(request);
            return Ok(res);
        }

        [HttpGet("authorization-test")]
        public async Task<IActionResult> AuthorizationTest([FromQuery] ExampleRequest request)
        {
            var res = await Mediator.Send(request);
            return Ok(res);
        }

        [HttpGet("authorization-test-access-manage-users")]
        [RequireClaim("Auth", "ManageUsers")]
        public async Task<IActionResult> AuthorizationTestManageUsers([FromQuery] ExampleRequest request)
        {
            var res = await Mediator.Send(request);
            return Ok(res);
        }

        [HttpGet("authorization-test-access-nonexistent")]
        [RequireClaim("Auth", "ThisClaimDoesNotExist")]
        public async Task<IActionResult> AuthorizationTestAccessNonExistentArea([FromQuery] ExampleRequest request)
        {
            var res = await Mediator.Send(request);
            return Ok(res);
        }
    }
}
