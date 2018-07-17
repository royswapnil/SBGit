using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SterlingBankLMS.Web.ViewModels
{
    public abstract class BaseValidatableModel : IValidatableObject
    {
        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}