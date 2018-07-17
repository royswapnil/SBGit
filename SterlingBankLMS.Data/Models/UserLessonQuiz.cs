
using SterlingBankLMS.Data.Models.Entities;
using System;

namespace SterlingBankLMS.Data.Models
{
    public class UserLessonQuiz : TrackableEntity
    {
        public int UserCourseId { get; set; }
        public UserCourse UserCourse { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsFinished { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }

    public static class UserLessonQuizExtensions{

        public static bool IsNull(this UserLessonQuiz lessonQuiz)
        {
            return lessonQuiz == null;
        }
    }
}