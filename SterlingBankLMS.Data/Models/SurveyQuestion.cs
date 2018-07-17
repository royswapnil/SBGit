using SterlingBankLMS.Data.Models.Enums;
using System.Collections.Generic;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class SurveyQuestion: OrganizationalBaseEntity
    {
        //public int SurveyId { get; set; }
        //public Survey Survey { get; set; }
        public string Question { get; set; }
        public AnswerType AnswerType { get; set; }
        public ICollection<SurveyQuestionOptions> Options { get; set; }
        public int SortOrder { get; set; }
        public int TemplateId { get; set; }
        public SurveyTemplate Template { get; set; }
        public bool IsMultipleChoice { get; set; }
    }
}