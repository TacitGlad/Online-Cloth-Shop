namespace Obaju.Models.ViewModels
{
    public class ProductListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string FrontImage { get; set; }

        public string BackImage { get; set; }

        public bool OnSale { get; set; } = false;

        public bool IsNew { get; set; } = false;

        public bool IsGift { get; set; } = false;
    }
}
