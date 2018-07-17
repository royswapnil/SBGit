using SterlingBankLMS.Data.Models.Enums;
using System.Collections.Generic;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Lesson : OrganizationalBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentUrl { get; set; }

        public bool IsExternalContent { get; set; }
        public string HtmlContent { get; set; }
        public string MimeType { get; set; }

        public decimal? PassMark { get; set; }

        public int? QuestionNo { get; set; }

        public bool IsGradeableContent { get; set; }
        public ICollection<QuizQuestion> Questions { get; set; }

        public LessonContentType LessonContentType { get; set; }

        public int? QuizRetakeCount { get; set; }

        public int SortOrder { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; }

    }
}