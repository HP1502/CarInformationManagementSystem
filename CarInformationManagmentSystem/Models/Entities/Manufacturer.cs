using CarInformationManagmentSystem.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace CarInformationManagmentSystem.Models.Entities
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        [ContactNumber]
        public string ContactPerson { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Registered Office address cannot exceed 200 characters.")]
        public string RegisteredOffice { get; set; } 
    }
}
