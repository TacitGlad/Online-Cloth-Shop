using Obaju.Constants;
using System.ComponentModel.DataAnnotations;

namespace Obaju.Models.BindingModels
{
    public class ChangePasswordBindingModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        [Required(ErrorMessage = ErrorMessage.PasswordRequired)]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [Required(ErrorMessage = ErrorMessage.PasswordRequired)]
        [StringLength(30, ErrorMessage = ErrorMessage.PasswordLength, MinimumLength = 4)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = ErrorMessage.ConfirmPasswordCompare)]
        [DataType(DataType.Password)]
        [Display(Name = "Retype new password")]
        public string RetypeNewPassword { get; set; }
    }
}
