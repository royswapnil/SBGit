using SterlingBankLMS.Web.Utilities.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SterlingBankLMS.Web.ViewModels
{
    public class LoginModel : BaseValidatableModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Email)) {
                yield return new ValidationResult("Email is required.");
            }
            else if (!Email.IsValidEmail()) {
                yield return new ValidationResult("Email is invalid.");
            }

            if (string.IsNullOrWhiteSpace(Password)) {
                yield return new ValidationResult("Password is required.");
            }
        }
    }


    public class ForgetPasswordModel : BaseValidatableModel
    {
        public string Email { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Email)) {
                yield return new ValidationResult("Email is required.");
            }

            else if (!Email.IsValidEmail()) {
                yield return new ValidationResult("Email is invalid.");
            }
        }
    }


    public class AdLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}