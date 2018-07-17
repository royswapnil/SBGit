using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class ReportUsers : OrganizationalBaseEntity
    {
        public Report Report { get; set; }
        public int? ReportId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
