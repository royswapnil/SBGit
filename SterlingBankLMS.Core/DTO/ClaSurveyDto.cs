using System.Collections.Generic;

namespace SterlingBankLMS.Core.DTO
{
    public class ClaSurveyDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public IEnumerable<ClaSurveyQuestionDto> Questions { get; set; }
    }
}