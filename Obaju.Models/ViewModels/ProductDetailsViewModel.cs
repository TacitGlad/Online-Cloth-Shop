using System.Collections.Generic;

namespace Obaju.Models.ViewModels
{
    public class ProductDetailsViewModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Details { get; set; }

        public IList<string> Images { get; set; }
    }
}
