using System.Collections.Generic;

namespace SterlingBankLMS.Core.DTO
{
    public class ClaSurveyQuestionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public IEnumerable<ClaSurveyQuestionOptionDto> Options { get; set; }
    }
}