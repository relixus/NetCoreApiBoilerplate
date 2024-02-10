using NetCoreApiBoilerplate.Areas.Common.Models;
using System.Net.Mail;
using System.Net;

namespace NetCoreApiBoilerplate.BLL.Areas.Common.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(SendEmailParams par)
        {
            var smptclient = _config["Email:SmtpClient"];
            var clientport = Convert.ToInt32(_config["Email:SmtpClientPort"]);
            var username = _config["Email:Username"];
            var password = _config["Email:Password"];

            var client = new SmtpClient(smptclient, clientport)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };
            client.Send(par.From, par.To, par.Subject, par.Message);
        }
    }
}