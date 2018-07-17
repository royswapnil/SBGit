using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
  public  class NotificationTypes
    {
        public int Id { get; set; }
        public NotificationType NotificationType { get; set; }

        public string NotificationAction { get; set; }

        public string ActionURL { get; set; }

        public bool ActionURLEnabled { get; set; }
        
    }
}
