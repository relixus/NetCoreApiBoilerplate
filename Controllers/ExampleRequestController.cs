using NetCoreApiBoilerplate.Application.Samples;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("get-example-2")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetSampleRequest2([FromQuery] ExampleRequest request)
        {
            var res = await Mediator.Send(request);
            return Ok(res);
        }
    }
}
