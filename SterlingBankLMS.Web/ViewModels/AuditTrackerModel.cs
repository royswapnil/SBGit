using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
    public class AuditTrackerModel
    {
        public int TotalRecords { get; set; }
        public int SeqId { get; set; }
        public int Trn { get; set; }
        public string OperationId { get; set; }
        public DateTime ChangedDateTime { get; set; }
        public string Name { get; set; }
        public string TableName { get; set; }
        public string AliasName { get; set; }
        public string ColumnName { get; set; }
        public string Operation { get; set; }
        public string BeforeUpdate { get; set; }
        public string AfterUpdate { get; set; }
    }

}