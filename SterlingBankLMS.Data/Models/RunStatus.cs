using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class RunStatus
    {

        public int Id { get; set; }
        public DateTime LastRunDate { get; set; }

        public string LastRunStatus { get; set; }

        public string RunTables { get; set; }

        public string RunFieldId { get; set; }

        public NotificationTypes NotificationType { get; set; }

        public int NotificationTypeId { get; set; }

    }
}
