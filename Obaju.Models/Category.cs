using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Obaju.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Type { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}