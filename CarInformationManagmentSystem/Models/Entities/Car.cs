using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarInformationManagmentSystem.Models.Validators;

namespace CarInformationManagmentSystem.Models.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        [Required]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }


        [Required]
        public int TypeId { get; set; }
        public virtual CarType Type { get; set; }

        [Required]
        [EngineFormat]
        [StringLength(4)]
        public string Engine { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "BHP must be a positive number.")]
        public int BHP { get; set; }

        [Required]
        public int TransmissionId { get; set; }
        public virtual CarTransmissionType Transmission { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Mileage must be a non-negative number.")]
        public int Mileage { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Seat count must be a number between 1 and 100.")]
        public int Seat { get; set; }

        [Required]
        [StringLength(100)]
        public string AirBagDetails { get; set; }

        [Required]
        [StringLength(100)]
        public string BootSpace { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}