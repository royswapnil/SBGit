using SterlingBankLMS.Data.Models.Enums;

namespace SterlingBankLMS.Core.DTO
{
    public class LessonNameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModuleId { get; set; }
        public LessonContentType Type { get; set; }
    }
}