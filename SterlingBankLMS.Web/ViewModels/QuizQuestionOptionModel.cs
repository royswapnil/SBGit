namespace SterlingBankLMS.Web.ViewModels
{
    public class QuizQuestionOptionModel:BaseModel
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public bool IsAnswer { get; set; }
    }

    public class ClaQuizOptionModel {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}