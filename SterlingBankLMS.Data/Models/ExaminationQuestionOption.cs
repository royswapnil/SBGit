namespace SterlingBankLMS.Data.Models.Entities
{
    public class ExaminationQuestionOption: TrackableEntity
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public bool IsAnswer { get; set; }

        public int ExaminationQuizId { get; set; }
        public ExaminationQuestion ExaminationQuiz { get; set; }
    }
}