using SterlingBankLMS.Data.Models.Enums;
using System.Collections.Generic;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class QuizQuestion : OrganizationalBaseEntity
    {
        public string Question { get; set; }
        public AnswerType AnswerType { get; set; }
        public ICollection<QuizQuestionOption> Options { get; set; }
        public int SortOrder { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public bool IsMultipleChoice { get; set; }
        public int Weight { get; set; }
    }


    public static class QuizQuestionExtensions
    {
        public static bool IsMultiple(this QuizQuestion question)
        {
            return question.AnswerType == AnswerType.CheckBox;
        }

        public static bool IsNull(this QuizQuestion quiz)
        {
            return quiz == null;
        }
    }
}