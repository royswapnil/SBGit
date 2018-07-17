using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class ReportFieldSort : OrganizationalBaseEntity
    {
        public Report Report { get; set; }
        public int? ReportId { get; set; }
        public string ColumnName { get; set; }
        public string Sort { get; set; }
    }
}
