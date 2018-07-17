using SterlingBankLMS.Data.Models.Entities;

namespace SterlingBankLMS.Data.Models
{
    public class UserExamination : TrackableEntity
    {
        public int? UserId { get; set; }
        public User User { get; set; }

        public int Attempt { get; set; }

        public int? ExtraAttempt { get; set; }

        public int ExaminationId { get; set; }
        public Examination Examination { get; set; }

        public int? LearningGroupId { get; set; }
        public LearningGroup LearningGroup { get; set; }
    }
}