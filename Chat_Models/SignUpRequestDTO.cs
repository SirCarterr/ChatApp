using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Models
{
    public class SignUpRequestDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-z0-9_.-]+@[a-zA-z0-9-]+.[a-zA-z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confrim password is not matched")]
        public string ConfirmPassword { get; set; }
    }
}
