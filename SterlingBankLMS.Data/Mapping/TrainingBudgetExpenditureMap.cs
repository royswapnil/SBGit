using SterlingBankLMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Mapping
{
    public class TrainingBudgetExpenditureMap : EntityTypeConfiguration<TrainingBudgetExpenditure>
    {
        public TrainingBudgetExpenditureMap()
        {
            HasRequired(x => x.Training).WithMany().HasForeignKey(x => x.TrainingId).WillCascadeOnDelete(false);
            HasRequired(x => x.Group).WithMany().HasForeignKey(x => x.GroupId).WillCascadeOnDelete(false);
            HasRequired(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).WillCascadeOnDelete(false);
            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
        }
    }
}
