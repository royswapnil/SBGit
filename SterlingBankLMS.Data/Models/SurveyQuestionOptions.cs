namespace SterlingBankLMS.Data.Models.Entities
{
    public class SurveyQuestionOptions: OrganizationalBaseEntity
    {
        public int SurveyQuestionId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public bool IsAnswer { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
    }
}
