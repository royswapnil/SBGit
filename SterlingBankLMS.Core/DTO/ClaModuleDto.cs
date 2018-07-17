using System.Collections.Generic;

namespace SterlingBankLMS.Core.DTO
{
    public class ClaModuleDto
    {
        public int Id { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public IEnumerable<ClaLessonDto> Lessons { get; set; }
    }
}