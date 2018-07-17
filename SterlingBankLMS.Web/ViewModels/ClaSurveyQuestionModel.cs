using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class ClaSurveyQuestionModel
    {
        public string Question { get; set; }
        public int Id { get; set; }
        public string AnswerType { get; set; }
        public int SortOrder { get; set; }
        public bool IsCurrent { get; set; }
        public ICollection<ClaSurveyOptionModel> Options { get; set; }
    }
}