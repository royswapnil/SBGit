namespace SterlingBankLMS.Web.Infrastructure.Messaging.Email
{
    public sealed class MailerConfig
    {
        public bool SmtpEnableSSl { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpUserName { get; set; }
        public bool SmtpUseDefaultCredentials { get; set; }
    }
}