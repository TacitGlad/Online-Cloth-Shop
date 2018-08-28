using System.ComponentModel.DataAnnotations;

namespace Obaju.Models.BindingModels
{
    public class SubscribeBindingModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
