using SterlingBankLMS.Data.Models.Enums;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class LearningGroupExam : TrackableEntity
    {
        public int ExaminationId { get; set; }
        public Examination Examination { get; set; }

        public int LearningGroupId { get; set; }
        public LearningGroup LearningGroup { get; set; }

    }
}