using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class NotificationRoleUsers:OrganizationalBaseEntity
    {
        public int UserId { get; set; }

        public int RoleId { get; set; } 

        public User User { get; set; }

        public Notification Notification { get; set; }

        public int NotificationId { get; set; }
    }
}
