using System.ComponentModel.DataAnnotations;

namespace Obaju.Models
{
    public class Subscriber
    {
        public int Id { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
