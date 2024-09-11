using CarInformationManagmentSystem.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace CarInformationManagmentSystem.Models.Entities
{
    public class CarTransmissionType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [TransmissionType]
        public string Name { get; set; }
    }
}
