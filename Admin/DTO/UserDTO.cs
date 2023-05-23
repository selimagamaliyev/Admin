using Admin.Validations;
using System.ComponentModel.DataAnnotations;

namespace Admin.DTO
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50)]
        public string Name { get; set; }


        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50)]
        public string SurName { get; set; }


        [Range(0, 99, ErrorMessage = "An age between 0 and 99 must be specified")]
        public byte Age { get; set; }


        [Required(ErrorMessage = "Genre must be specified")]
        [MaxLength(10)]
        public string Genre { get; set; }


        [EmailAddress]
        [Required(ErrorMessage = "Email must be specified")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pasword must be specified")]
        [DataType(DataType.Password)]
        public string Pasword { get; set; }


        [DataType(DataType.Password)]
        [Compare("Pasword", ErrorMessage = "The password and confirmation pasword do not match")]
        public string ConfirmPasword { get; set; }


        [CustomPhoneNumberValidation]
        public string Number { get; set; }
    }
}
