using Obaju.Models.ViewModels;
using System.Collections.Generic;

namespace Obaju.Services.Interfaces
{
    public interface IProductService
    {
        IList<CategoriesViewModel> GetCategoris();

        IList<CategoriesWithProductsCountViewModel> GetCategorisWithProductCount();

        IList<BrandsViewModel> GetBrands();

        IList<ProductListViewModel> GetProductList(string categoryGender, string categoryName);
    }
}
