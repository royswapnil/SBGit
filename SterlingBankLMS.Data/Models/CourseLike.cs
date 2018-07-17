using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;

namespace SterlingBankLMS.Data.Models
{
    public class CourseLike : TrackableEntity
    {
        public CourseLikeStatus Status { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}