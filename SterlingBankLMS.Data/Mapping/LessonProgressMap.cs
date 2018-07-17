using SterlingBankLMS.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class LessonProgressMap:EntityTypeConfiguration<LessonProgress>
    {
        public LessonProgressMap()
        {
            HasRequired(x => x.Module).WithMany().HasForeignKey(x => x.ModuleId).WillCascadeOnDelete(false);
            HasRequired(x => x.UserCourse).WithMany().HasForeignKey(x => x.UserCourseId);
        }
    }
}