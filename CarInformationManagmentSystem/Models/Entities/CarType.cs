using CarInformationManagmentSystem.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace CarInformationManagmentSystem.Models.Entities
{
    public class CarType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [CarType]
        public string Type { get; set; }

        public string ContactPerson { get; set; }

    }
}
