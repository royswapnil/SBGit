using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class LearningGroupExamMap : EntityTypeConfiguration<LearningGroupExam>
    {
        public LearningGroupExamMap()
        {
            HasRequired(x => x.Examination).WithMany().HasForeignKey(x => x.ExaminationId).WillCascadeOnDelete(false);
            HasRequired(x => x.LearningGroup).WithMany(x => x.Exams).HasForeignKey(x => x.LearningGroupId).WillCascadeOnDelete(false);
        }
    }
}