using System;
using System.ComponentModel.DataAnnotations;

namespace CarInformationManagmentSystem.Models.Validators
{
    public class CarTypeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var carType = value as string;

            if (carType == "Hatchback" || carType == "Sedan" || carType == "SUV")
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Type must be either 'Hatchback', 'Sedan', or 'SUV'.");
        }
    }
}
