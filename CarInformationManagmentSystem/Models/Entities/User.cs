using System.ComponentModel.DataAnnotations;

namespace CarInformationManagmentSystem.Models.Entities
{
    public class User
    {
        [Key]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public String UserRole { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string EMail { get; set; }

        [Required]
        public DateOnly DOB { get; set; }

    }
}