namespace NetCoreApiBoilerplate.Areas.Common.Models
{
    public class SendEmailParams
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
