using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obaju.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        
        [Required]
        public string Details { get; set; }

        [Required]
        public string Color { get; set; }

        public int Quantity { get; set; }

        [NotMapped]
        public bool InStock { get => this.Quantity != 0; }

        public DateTime CreationTime { get; set; } = DateTime.Now;

        // One-to-many
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        // Many-to-one
        public ICollection<Image> Images { get; set; } = new List<Image>();

        // Many-to-many
        public ICollection<OrderProduct> Orders { get; set; } = new List<OrderProduct>();

        // Many-to-many
        public ICollection<UserWishlist> Users { get; set; } = new List<UserWishlist>();

        
    }
}
