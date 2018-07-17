using SterlingBankLMS.Data.Models.Enums;
using System.Collections.Generic;

namespace SterlingBankLMS.Core.DTO
{
    public class QuizQuestionDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public AnswerType AnswerType { get; set; }
        public ICollection<QuizOptionDto> Options { get; set; }
        public int SortOrder { get; set; }
    }
}