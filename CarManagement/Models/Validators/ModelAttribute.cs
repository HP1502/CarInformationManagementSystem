using CarManagement.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarManagement.Models.Validators
{
    public class ModelAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = value as string;
            CarInformationSystemContext c = new CarInformationSystemContext();

            if (c.CAR.Any(c => c.Model == model))
            {
                return new ValidationResult("Model name must be unique.");
            }
            return ValidationResult.Success;
        }
    }
}
