using CarManagement.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace CarManagement.Models
{
    public class ManufacturerClass
    {
        [Key]  // Primary Key (System Generated)
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }  // Unique

        [Required]
        [ContactNumber]
        public string ContactNo { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Registered Office address cannot exceed 200 characters.")]
        public string RegisteredOffice { get; set; }
        //public ICollection<Car> Cars { get; set; }  // Navigation Property 
    }
}
