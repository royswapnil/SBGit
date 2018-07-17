using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class ExaminationAttemptMap : EntityTypeConfiguration<ExaminationAttempt>
    {
        public ExaminationAttemptMap()
        {
            //HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            //HasRequired(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).WillCascadeOnDelete(false);
            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
        }
    }
}