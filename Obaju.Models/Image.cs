using System.ComponentModel.DataAnnotations;

namespace Obaju.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Path Required")]
        public string Path { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
