using SterlingBankLMS.Data.Models.Entities;
using System;

namespace SterlingBankLMS.Data.Models
{
    public class LessonProgress:TrackableEntity
    {
        public int UserCourseId { get; set; }
        public int LessonId { get; set; }
        public int? ModuleId { get; set; }

        public string Name { get; set; }

        public bool IsCompleted { get; set; }
     
        public DateTime? EndDate { get; set; }

        public Lesson Lesson { get; set; }
        public UserCourse UserCourse { get; set; }

        public Module Module { get; set; }
    }
}