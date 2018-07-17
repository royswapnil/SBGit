using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SterlingBankLMS.Web.Utilities;

namespace SterlingBankLMS.Web.ViewModels
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public bool IsForEmployee { get; set; }
        public bool IsForLD { get; set; }
        public bool IsForHR { get; set; }
        public bool IsForManagement { get; set; }
        public bool IsForIT { get; set; }
        public string MailMessage { get; set; }
        public string MailSubject { get; set; }
        public string NotificationMessage { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool CanIgnoreMail { get; set; }
        public bool IsMail { get; set; }
        public bool IsNotification { get; set; }
        public bool MailSetupDisabled { get; set; }
        public string NotficationFormat => CommonHelper.GetDescription(NotificationType);
        public HashSet<NotificationReplaceTags> ReplacementTags { get; set; }

    }

    public class UserNotification
    {
        public string Message { get; set; }
        public NotificationType NotificationType { get; set; }
        public string NotficationFormat => CommonHelper.GetDescription(NotificationType);
        public DateTime CreatedDate { get; set; }
        public string CreatedDateFormat => CreatedDate.ToString("ddd, dd MMM yyy");

    }
}
