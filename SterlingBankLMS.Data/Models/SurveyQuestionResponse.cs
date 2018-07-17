namespace SterlingBankLMS.Data.Models.Entities
{
    public class SurveyQuestionResponse : TrackableEntity
    {
        public SurveyQuestion SurveyQuestion { get; set; }
        public UserSurvey UserSurvey { get; set; }

        public int UserSurveyId { get; set; }

        public int SurveyQuestionId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public string Value { get; set; }
    }
}