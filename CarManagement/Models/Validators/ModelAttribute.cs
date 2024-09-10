<<<<<<< HEAD
﻿using CarManagement.Data;
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

            // Set up configuration to get connection string
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var cs = config.GetConnectionString("DefaultConnection");

            // Create DbContextOptions with SQL Server provider
            var optionsBuilder = new DbContextOptionsBuilder<CarInformationSystemContext>();
            optionsBuilder.UseSqlServer(cs);
            // Specify the database provider and connection string (for SQL Server in this case)

            CarInformationSystemContext c = new CarInformationSystemContext(optionsBuilder.Options);

            if (c.CAR.Any(c => c.Model == model))
            {
                return new ValidationResult("Model name must be unique.");
            }
            return ValidationResult.Success;
        }
    }
}
=======
﻿using CarManagement.Data;
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
>>>>>>> 88eac82cbc7e90fd6a90fc7c710ee8208f6cf91f
