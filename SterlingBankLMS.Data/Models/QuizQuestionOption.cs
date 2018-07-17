namespace SterlingBankLMS.Data.Models.Entities
{
    public class QuizQuestionOption: OrganizationalBaseEntity
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public bool IsAnswer { get; set; }
        public QuizQuestion Quiz { get; set; }
    }
}