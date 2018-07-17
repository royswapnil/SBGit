using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class DepartmentBudgetMap : EntityTypeConfiguration<DepartmentBudget>
    {
        public DepartmentBudgetMap()
        {
            HasRequired(x => x.Group).WithMany().HasForeignKey(x => x.GroupId).WillCascadeOnDelete(false);
            HasRequired(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).WillCascadeOnDelete(false);
            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
        }
    }
}