using CarManagement.Models.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarManagement.Models
{
    public class Car
    {
        [Key]  // Primary Key (System Generated)
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        [Required]
        public int ManufacturerId { get; set; }  // Foreign Key

        [ForeignKey("ManufacturerId")]
        //public Manufacturer Manufacturer { get; set; }  // Navigation Property

        [Required]
        public int TypeId { get; set; }  // Foreign Key

        [ForeignKey("TypeId")]
        //public CarType CarType { get; set; }  // Navigation Property

        [Required]
        [EngineFormat]
        [StringLength(4)]
        public string Engine { get; set; }  // Check Constraint

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "BHP must be a positive number.")]
        public int BHP { get; set; }

        [Required]
        public int TransmissionId { get; set; }  // Foreign Key

        [ForeignKey("TransmissionId")]
        //public CarTransmissionType TransmissionType { get; set; }  // Navigation Property

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Mileage must be a non-negative number.")]
        public int Mileage { get; set; }  // Not Null

        [Required]
        [Range(1, 100, ErrorMessage = "Seat count must be a number between 1 and 100.")]
        public int Seat { get; set; }  // Not Null

        [Required]
        [StringLength(100)]
        public string AirBagDetails { get; set; }  // Not Null

        [Required]
        [StringLength(100)]
        //[Range(0, int.MaxValue, ErrorMessage = "BootSpace must be a non-negative number.")]
        public string BootSpace { get; set; }  // Not Null

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }  // Not Null
    }
}