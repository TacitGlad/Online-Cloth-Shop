using Obaju.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Obaju.Services.Interfaces
{
    public interface IProductManager
    {
        Task<List<NavCategoriesViewModel>> GetNavCategorisAsync();

        Task<List<PanelCategoriesViewModel>> GetPanelCategorisAsync();

        Task<List<BrandsViewModel>> GetBrandsAsync();

        Task<IList<ProductListViewModel>> GetProductList(string gender, string category, IList<string> brandsFilter);

        Task<ProductDetailsViewModel> GetProductDetailsAsync(int id);
    }
}
