using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obaju.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal Total { get; set; }

        [Required]
        public string InoviceAddress { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public string DeliveryMethod { get; set; } = "One-day delivery";

        [Required]
        public string PaymentMethod { get; set; } = "Cash on delivery";

        public int UserId { get; set; }

        public User User { get; set; }

        // Many-to-many
        public ICollection<OrderProduct> Products { get; set; } = new List<OrderProduct>();
    }
}