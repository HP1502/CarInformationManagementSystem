using CarManagement.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace CarManagement.Models
{
    public class CarTypeClass
    {
        [Key]  // Primary Key (System Generated)
        public int Id { get; set; }

        [Required]
        [CarType]
        public string Type { get; set; }

        //public ICollection<Car> Cars { get; set; }  // Navigation Property
    }
}
