using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class ClaExamQuestionModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string AnswerType { get; set; }
        public bool IsCurrent { get; set; }
        public List<ClaExamOptionModel> Options { get; set; }
    }
}