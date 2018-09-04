using Obaju.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Obaju.Services.Interfaces
{
    public interface IProductManager
    {
        IList<CategoriesViewModel> GetCategoris();

        IList<CategoriesWithProductsCountViewModel> GetCategorisWithProductCount();

        IList<BrandsViewModel> GetBrands();

        Task<IList<ProductListViewModel>> GetProductList(string categoryGender, string categoryName);

        Task<ProductDetailsViewModel> GetProductDetailsAsync(int id);
    }
}
