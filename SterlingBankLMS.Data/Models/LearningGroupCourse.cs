using SterlingBankLMS.Data.Models.Enums;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class LearningGroupCourse: TrackableEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int LearningGroupId { get; set; }
        public LearningGroup LearningGroup { get; set; }
        public UserCourseAvailability Availability { get; set; }

    }
}