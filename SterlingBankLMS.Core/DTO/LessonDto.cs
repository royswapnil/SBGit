using SterlingBankLMS.Data.Models.Enums;

namespace SterlingBankLMS.Core.DTO
{
    public class LessonDto
    {
        public int CourseId { get; set; }
        public int ModuleId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentUrl { get; set; }
        public int? QuizRetakeCount { get; set; }
        public int? QuestionNo { get; set; }

        public bool IsExternalContent { get; set; }
        public string HtmlContent { get; set; }
        public string MimeType { get; set; }

        public LessonContentType LessonContentType { get; set; }

        public int SortOrder { get; set; }
    }

    public static class LessonDtoExtensions
    {
        public static bool Is(this LessonDto lesson, LessonContentType type)
        {
            return lesson.LessonContentType == type;
        }

        public static bool IsNull(this LessonDto lesson)
        {
            return lesson == null;
        }
    }
}