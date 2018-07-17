using SterlingBankLMS.Data.Models.Enums;
using System.Collections.Generic;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Notification: OrganizationalBaseEntity
    {
        public string MailMessage { get; set; }
        public string MailSubject { get; set; }
        public string NotificationMessage { get; set; }
        public Enums.NotificationType NotificationType { get; set; }
        public bool IsForEmployee { get; set; }
        public bool IsForLD { get; set; }
        public bool IsForHR { get; set; }
        public bool IsForManagement { get; set; }
        public bool IsForIT { get; set; }
        public string RedirectUrl { get; set; }
        public bool CanIgnoreMail { get; set; }
        public bool IsMail { get; set; }
        public bool IsNotification { get; set; }
        public ICollection<NotificationRoleUsers> NotificationRoleUsers { get; set; }
        public bool MailSetupDisabled { get; set; }
        public string UsedTags { get; set; }

    }
}
