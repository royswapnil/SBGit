using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class MessageQueue : OrganizationalBaseEntity
    {
        public bool IsProccessed { get; set; }

        public NotificationType NotificationType { get; set; }

        public int ActionId { get; set; }

        public string Comments { get; set; }

        public string AdditionalUsers { get; set; }

        public int ReceiverId { get; set; }
    }
}
