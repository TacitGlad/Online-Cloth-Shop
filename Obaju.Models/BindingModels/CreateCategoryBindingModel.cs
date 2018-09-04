using System.ComponentModel.DataAnnotations;

namespace Obaju.Models.BindingModels
{
    public class CreateCategoryBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Group { get; set; }
    }
}
