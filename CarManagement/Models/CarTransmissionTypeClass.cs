using CarManagement.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace CarManagement.Models
{
    public class CarTransmissionTypeClass
    {
        [Key]  // Primary Key (System Generated)
        public int Id { get; set; }

        [Required]
        [TransmissionType]
        public string Type { get; set; }

        //public ICollection<Car> Cars { get; set; }  // Navigation Property
    }
}
