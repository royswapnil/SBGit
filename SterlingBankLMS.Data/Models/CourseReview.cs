using SterlingBankLMS.Data.Models.Entities;

namespace SterlingBankLMS.Data.Models
{
    public class CourseReview : TrackableEntity
    {
        public float Rating { get; set; }
        public string Review { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}