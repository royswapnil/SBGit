using SterlingBankLMS.Web.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SterlingBankLMS.Web.Utilities.Extensions
{
    public static class ValidationExtensions
    {
        public static bool IsValid<T>(this T source, out List<string> errorList) where T : BaseValidatableModel
        {
            var errors = new List<string>();

            var validationResults = source.Validate(new ValidationContext(source, null, null));

            errors.AddRange(validationResults.Select(e => e.ErrorMessage));

            errorList = errors;
            return !errors.Any();
        }
    }
}