using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class LineManagerMap: EntityTypeConfiguration<LineManager>
    {
        public LineManagerMap()
        {
            HasRequired(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId).WillCascadeOnDelete(false);
            HasRequired(x => x.LIneManager).WithMany().HasForeignKey(x => x.LineManagerId).WillCascadeOnDelete(false);

            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).WillCascadeOnDelete(false);
        }
        
    }
}
