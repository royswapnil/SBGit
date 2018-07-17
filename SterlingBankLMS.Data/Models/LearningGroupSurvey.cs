using SterlingBankLMS.Data.Models.Enums;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class LearningGroupSurvey : TrackableEntity
    {
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public int LearningGroupId { get; set; }
        public LearningGroup LearningGroup { get; set; }
    }
}