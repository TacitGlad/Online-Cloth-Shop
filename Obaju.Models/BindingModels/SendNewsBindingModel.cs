using System.ComponentModel.DataAnnotations;

namespace Obaju.Models.BindingModels
{
    public class SendNewsBindingModel
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
