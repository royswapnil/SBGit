using SterlingBankLMS.Data.Models.Enums;
using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class QuizQuestionModel:BaseModel
    {
       
        public string Question { get; set; }
        public AnswerType AnswerType { get; set; }
        public List<QuizQuestionOptionModel> Options { get; set; }
        public int SortOrder { get; set; }
        public int LessonId { get; set; }
        public int ExaminationId { get; set; }
        public int TemplateId { get; set; }
        public bool IsMultipleChoice { get; set; }
        public int Weight { get; set; }
       
    }
}