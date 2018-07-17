using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SterlingBankLMS.Web.ViewModels
{

    public class ClaExamFinishModel
    {
        public int ExamId { get; set; }
        public int CourseId { get; set; }
    }

    public class ClaExamResponseModel: BaseValidatableModel
    {
        public int ExamId { get; set; }
        public int CourseId { get; set; }

        public int QuestionId { get; set; }

        public int[] Answers { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Answers == null || !Answers.Any()) {
                yield return new ValidationResult("Invalid submission !");
            }
        }
    }
}