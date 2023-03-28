using NetCoreApiBoilerplate.Areas.Common.Models;

namespace NetCoreApiBoilerplate.Areas.Common.Services
{
    public interface IEmailService
    {
        public void SendEmail(SendEmailParams par);
    }
}
