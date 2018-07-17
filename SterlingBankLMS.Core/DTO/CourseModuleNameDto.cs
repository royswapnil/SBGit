using System.Collections.Generic;

namespace SterlingBankLMS.Core.DTO
{
    public class CourseModuleNameDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int SortOrder { get; set; }

        public IEnumerable<LessonNameDto> Lessons { get; set; }
    }
}