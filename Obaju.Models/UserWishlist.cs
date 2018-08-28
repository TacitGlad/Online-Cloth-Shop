using System;

namespace Obaju.Models
{
    public class UserWishlist
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}