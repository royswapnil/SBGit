using SterlingBankLMS.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class UserLessonQuizMap:EntityTypeConfiguration<UserLessonQuiz>
    {
        public UserLessonQuizMap()
        {
            HasRequired(x => x.UserCourse).WithMany().HasForeignKey(x => x.UserCourseId).WillCascadeOnDelete(false);

        }
    }
}