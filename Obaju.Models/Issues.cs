using System.ComponentModel.DataAnnotations;

namespace Obaju.Models
{
    public class Issues
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public bool IsOpen { get; set; } = true;
    }
}
