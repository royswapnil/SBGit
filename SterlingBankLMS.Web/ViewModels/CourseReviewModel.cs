using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SterlingBankLMS.Web.ViewModels
{
    public class CourseReviewModel : BaseValidatableModel
    {
        public int? Rating { get; set; }
        public string Review { get; set; }
        public int CourseId { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Rating != null && Rating > 5) {
                yield return new ValidationResult("Ratings value is invalid.");
            }
        }
    }
}