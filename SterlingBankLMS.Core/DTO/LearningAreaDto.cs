using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;

namespace SterlingBankLMS.Core.DTO
{
    public class LearningAreaDto:BaseEntity
    {
        public LearningArea LearningArea { get; set; }
        public int CourseCount { get; set; }
    }
}
