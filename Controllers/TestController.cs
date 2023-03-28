using Microsoft.AspNetCore.Mvc;
using NetCoreApiBoilerplate;
using NetCoreApiBoilerplate.Areas.Common.Models;
using NetCoreApiBoilerplate.Areas.Common.Services;

namespace DotnetApiBoilerplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ApiController
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<TestController> _logger;
        private readonly IEmailService _emailService;
        public TestController(ILogger<TestController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpGet("SendTestEmail")]
        public IActionResult SendEmail()
        {
            _emailService.SendEmail(new SendEmailParams
            {
                From = "from@example.com",
                To = "to@example.com",
                Subject = "Hello world",
                Message = "Test Body"
            });

            return Ok("Sent");
        }
    }
}