using Obaju.Constants;
using System.ComponentModel.DataAnnotations;

namespace Obaju.Models.BindingModels
{
    public class LoginFormBindingModel
    {
        [EmailAddress]
        [Required(ErrorMessage = ErrorMessage.EmailRequired)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = ErrorMessage.PasswordRequired)]
        [StringLength(30, ErrorMessage = ErrorMessage.PasswordLength, MinimumLength = 4)]
        public string Password { get; set; }
    }
}
