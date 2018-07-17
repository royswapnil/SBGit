
using SterlingBankLMS.Data.Models.Enums;

namespace SterlingBankLMS.Core.DTO
{
    public class CourseProgressDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int CourseId { get; set; }
        public decimal Progress { get; set; }
        public UserCourseStatus CourseStatus { get; set; }
        public string LearningArea { get; set; }
    }
}