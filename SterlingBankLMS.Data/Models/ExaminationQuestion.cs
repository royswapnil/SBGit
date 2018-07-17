using SterlingBankLMS.Data.Models.Enums;
using System.Collections.Generic;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class ExaminationQuestion : TrackableEntity
    {
        public string Question { get; set; }
        public AnswerType AnswerType { get; set; }
        public ICollection<ExaminationQuestionOption> Options { get; set; }
        public int SortOrder { get; set; }
        public Examination Examination { get; set; }
        public int ExaminationId { get; set; }
        public bool IsMultipleChoice { get; set; }
        public int Weight { get; set; }
    }

    public static class ExaminationQuestionExtensions
    {
        public static bool IsMultiple(this ExaminationQuestion question)
        {
            return question.AnswerType == AnswerType.CheckBox;
        }
    }
}