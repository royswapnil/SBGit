namespace SterlingBankLMS.Web.ViewModels
{
    public class SurveyRespsonseModel
    {
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public int[] Answers { get; set; }
        public string Answer { get; set; }
    }
}