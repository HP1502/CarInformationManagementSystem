using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CarInformationManagmentSystem.Models.Validators
{
    public class EngineFormatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var engine = value as string;

            // The regex pattern: 1st char = digit, 2nd char = '.', 3rd char = digit, 4th char = letter
            var regex = new Regex(@"^\d\.\dL$");

            if (engine != null && regex.IsMatch(engine))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Engine must be in format 'd.dX' where d is a digit and X is a letter.");
        }
    }
}