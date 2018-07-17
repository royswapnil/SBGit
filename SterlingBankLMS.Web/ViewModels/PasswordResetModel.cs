using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SterlingBankLMS.Web.ViewModels
{
    [Serializable]
    public class PasswordResetModel : BaseValidatableModel
    {
        public string Code { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Code) || string.IsNullOrWhiteSpace(Email)) {
                yield return new ValidationResult("Reset request is incomplete, Please try again.");
            }
        }
    }
}