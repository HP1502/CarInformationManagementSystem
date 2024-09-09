using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CarManagement.Models.Validators
{
    public class ContactNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var contactNo = value as string;

            // Ensure the contact number is 10 digits long
            var regex = new Regex(@"^\d{10}$");

            if (contactNo != null && regex.IsMatch(contactNo))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Contact number must be exactly 10 digits.");
        }
    }
}
