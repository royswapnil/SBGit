using System.Collections.Generic;
using System.Net.Mail;

namespace SterlingBankLMS.Web.Infrastructure.Messaging.Email
{
    public abstract class Mail
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public string Sender { get; set; }
        public string SenderDisplayName { get; set; }
        public bool IsBodyHtml { get; set; }
        public string BodyPath { get; set; }
        public bool BodyIsFile { get; set; }
        public ICollection<string> To { get; set; }
        public ICollection<string> Bcc { get; set; }
        public ICollection<string> CC { get; set; }
        public ICollection<Attachment> Attachments { get; set; }

        private Mail()
        {
            IsBodyHtml = true;
            To = new List<string>();
            CC = new List<string>();
            Bcc = new List<string>();
            Attachments = new List<Attachment>();
        }

        public Mail(string sender, string subject, params string[] to)
            : this()
        {
            Sender = sender;
            Subject = subject;

            foreach (var rec in to)
                To.Add(rec);
        }
    }

    public class ApplicationEmail : Mail
    {
        public ApplicationEmail(string sender, string subject, params string[] to) : base(sender, subject, to)
        {
        }
    }
}