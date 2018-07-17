using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
   public class MailAlertUsers:OrganizationalBaseEntity
    {
        public User User { get; set; }

        public int UserId { get; set; }

        public Notification Notification { get; set; }

        public int NotificationId { get; set; }
        
    }
}
