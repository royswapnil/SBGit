using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class LearningGroupCourseMap : EntityTypeConfiguration<LearningGroupCourse>
    {
        public LearningGroupCourseMap()
        {
            HasRequired(x => x.LearningGroup).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.LearningGroup).WithMany(x => x.Courses).HasForeignKey(x => x.LearningGroupId).WillCascadeOnDelete(false);
        }
    }
}