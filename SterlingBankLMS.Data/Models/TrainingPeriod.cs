using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class TrainingPeriod : OrganizationalBaseEntity
    {
        public Training Training { get; set; }
        public int TrainingId { get; set; }
        public DayOfWeek Day { get; set; }

        public string StartTime { get; set; }

        public TimeSpan? StartSpan { get; set; }

        public TimeSpan? EndSpan { get; set; }

        public string EndTime { get; set; }
    }
}
