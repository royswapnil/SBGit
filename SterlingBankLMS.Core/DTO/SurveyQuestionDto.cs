using System.Collections.Generic;

namespace SterlingBankLMS.Core.DTO
{
    public class SurveyQuestionDto
    {
        public int Id { get; set; }
        public int? SurveyId { get; set; }
        public string Question { get; set; }
        public ICollection<SurveyOptionDto> Options { get; set; }
        public int SortOrder { get; set; }
    }
}