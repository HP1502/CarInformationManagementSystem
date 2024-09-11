using System;
using System.ComponentModel.DataAnnotations;

namespace CarInformationManagmentSystem.Models.Validators
{
    public class TransmissionTypeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var transmission = value as string;

            if (transmission == "Manual" || transmission == "Automatic")
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Transmission must be either 'Manual' or 'Automatic'.");
        }
    }
}
