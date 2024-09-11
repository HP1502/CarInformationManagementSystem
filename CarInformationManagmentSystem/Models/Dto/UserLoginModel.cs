using System.ComponentModel.DataAnnotations;

namespace CarInformationManagmentSystem.Models.Dto
{
    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
