using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class ClaQuizQuestionModel
    {
        public string Question { get; set; }
        public string AnswerType { get; set; }
        public List<ClaQuizOptionModel> Options { get; set; }
        //public int SortOrder { get; set; }
        public bool IsCurrent { get; set; }
        public int Id { get; set; }
    }
}