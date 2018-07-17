using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class ExaminationQuizResponseMap : EntityTypeConfiguration<ExaminationQuizResponse>
    {
        public ExaminationQuizResponseMap()
        {
            HasRequired(x => x.ExaminationAttempt).WithMany().HasForeignKey(x => x.ExaminationAttemptId).WillCascadeOnDelete(false);

            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
           // HasRequired(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).WillCascadeOnDelete(false);
        }
    }
}