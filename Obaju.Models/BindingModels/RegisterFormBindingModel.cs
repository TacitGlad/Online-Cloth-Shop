using Obaju.Constants;
using System.ComponentModel.DataAnnotations;

namespace Obaju.Models.BindingModels
{
    public class RegisterFormBindingModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = ErrorMessage.FirstNameRequired)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = ErrorMessage.LastNameRequired)]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = ErrorMessage.EmailRequired)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = ErrorMessage.PasswordRequired)]
        [StringLength(30, ErrorMessage = ErrorMessage.PasswordLength, MinimumLength = 4)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = ErrorMessage.ConfirmPasswordCompare)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
