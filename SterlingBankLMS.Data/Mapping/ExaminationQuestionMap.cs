using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class ExaminationQuestionMap : EntityTypeConfiguration<ExaminationQuestion>
    {
        public ExaminationQuestionMap()
        {
            HasRequired(x => x.Examination).WithMany(x=>x.ExamQuestions).HasForeignKey(x => x.ExaminationId).WillCascadeOnDelete(false);
            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
            //HasRequired(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).WillCascadeOnDelete(false);
        }
    }
}