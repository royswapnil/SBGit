using SterlingBankLMS.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models
{
    public class TrainingBudgetExpenditure : OrganizationalBaseEntity
    {
        public int NumberOfParticipants { get; set; }
        public int DepartmentBudgetId { get; set; }
        public DepartmentBudget DepartmentBudget { get; set; }
        public int TrainingId { get; set; }
        public Training Training { get; set; }
        public Group Group { get; set; }
        public int GroupId { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public decimal AmountSpent { get; set; }
    }
}
