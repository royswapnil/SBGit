using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Mails : OrganizationalBaseEntity
    {
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
        public NotificationType MailType { get; set; }
        public int ActionId { get; set; }
        public string ActionURL { get; set; }
        public string Replacements { get; set; }
        public string Attachments { get; set; }
        public string Subject { get; set; }
        public string MailTo { get; set; }
        public string Bcc { get; set; }
        public string CC { get; set; }

        public string MailFrom { get; set; }
        public string MailToName { get; set; }
        public string MailFromName { get; set; }
        public string MailBody { get; set; }
        public bool IsSent { get; set; }
        public string ReplyTo { get; set; }
    }
}
