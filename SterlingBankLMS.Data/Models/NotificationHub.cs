using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class NotificationHub : OrganizationalBaseEntity
    {
        public NotificationType NotificationType { get; set; }
        public int? ActionId { get; set; }
        public string ActionURL { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public User Receiver { get; set; }
        public int ReceiverId { get; set; }
        public string Replacements { get; set; }
    }
}
