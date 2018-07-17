using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class ReportSchedule : OrganizationalBaseEntity
    {
        public Report Report { get; set; }
        public int? ReportId { get; set; }
        public string FrequencyType { get; set; }
        public DateTime? RunDate { get; set; }
        public string RunDay { get; set; }
        public string RunTime { get; set; }
        public string RunFrequency { get; set; }
        public DateTime? LastRunSuccessDate { get; set; }
        public DateTime? LastRunFailureDate { get; set; }
        public DateTime? NextRunDate { get; set; }

    }
}
