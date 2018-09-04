using System.ComponentModel.DataAnnotations;

namespace Obaju.Models.BindingModels
{
    public class CreateBrandBindingModel
    {
        [Required]
        public string Name { get; set; }
    }
}
