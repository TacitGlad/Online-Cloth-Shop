using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Obaju.Models.BindingModels
{
    public class CreateProductBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.01", "9999")]
        public decimal Price { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public string Color { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        [Required]
        public IList<IFormFile> Images { get; set; }
    }
}
