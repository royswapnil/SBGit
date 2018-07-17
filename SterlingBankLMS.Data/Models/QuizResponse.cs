namespace SterlingBankLMS.Data.Models.Entities
{
    public class QuizResponse : TrackableEntity
    {
        public int UserLessonQuizId { get; set; }
        public UserLessonQuiz UserLessonQuiz { get; set; }
        public QuizQuestion Quiz { get; set; }
        public int QuizId { get; set; }
        public string Value { get; set; }
        public bool IsAnswer { get; set; }
    }
}