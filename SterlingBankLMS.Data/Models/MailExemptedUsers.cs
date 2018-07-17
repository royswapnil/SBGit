using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public  class MailExemptedUsers:OrganizationalBaseEntity
    {
        public NotificationType MailType { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
