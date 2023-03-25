using NetCoreApiBoilerplate.Application.Auth;
using NetCoreApiBoilerplate.Application.Samples;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApiBoilerplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {
        [HttpPost("create-user")]
        public async Task<ActionResult<CreateUserResult>> GetSampleRequest([FromBody] CreateUserRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LogInUserRequest request)
        {
            var user = await Mediator.Send(request);
            if(user == null)
            {
                return NotFound(request);
            }
            return Ok(user);
        }

        [HttpPost("refresh-token")]
        public async Task<RefreshTokenResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            return await Mediator.Send(request);
        }
    }
}
