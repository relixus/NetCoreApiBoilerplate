using Microsoft.AspNetCore.Mvc;
using NetCoreApiBoilerplate;
using NetCoreApiBoilerplate.Areas.Common.Models;
using NetCoreApiBoilerplate.Areas.Common.Services;
using NetCoreApiBoilerplate.Repository;

namespace DotnetApiBoilerplate.Controllers
{

    /// <summary>
    /// SAMPLE IMPLEMENTATION OF REPOSITORY
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ApiController
    {
        private readonly ILogger<TestController> _logger;
        private readonly IEmailService _emailService;
        private readonly ApplicationRepository repository;
        public TestController(ILogger<TestController> logger, IEmailService emailService, ApplicationRepository repository)
        {
            _logger = logger;
            _emailService = emailService;
            this.repository = repository;
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

        [HttpGet("SampleDBTransactionScoped")]
        public ActionResult SampleDBTransactionScoped()
        {
            try
            {
                repository.BeginTransaction();
                repository.Sample.EFCoreSave();
                repository.Sample.SQLClientSave();
                repository.SaveChanges();
            }
            catch (Exception ex)
            {
                repository.Rollback();
                _logger.LogError($"Error @ SampleDBTransactionScoped: {ex.Message}", ex.ToString());
                return StatusCode(500,ex.ToString());
            }
            return Ok();
        }
    }
}