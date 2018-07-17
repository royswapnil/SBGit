using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.DTO
{
    public class NotificationDto
    {
       
        public int Id { get; set; }
        public string MailMessage { get; set; }
        public string MailSubject { get; set; }
        public string NotificationMessage { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool IsForEmployee { get; set; }
        public bool IsForLD { get; set; }
        public bool IsForHR { get; set; }
        public bool IsForManagement { get; set; }
        public bool IsForIT { get; set; }
        public bool CanIgnoreMail { get; set; }
        public bool IsMail { get; set; }
        public bool IsNotification { get; set; }
        public bool MailSetupDisabled { get; set; }
        public string NotficationFormat => AppHelper.GetDescription(NotificationType);
        public string UsedTags { get; set; }

    }
}
