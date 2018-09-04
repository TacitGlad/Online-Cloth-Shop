using Microsoft.AspNetCore.Mvc;
using Obaju.Services.Interfaces;
using System.Threading.Tasks;

namespace Obaju.App.ViewComponents
{
    public class CategoriesPanelViewComponent : ViewComponent
    {
        private readonly IProductManager _productService;

        public CategoriesPanelViewComponent(IProductManager productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           var categories = _productService.GetCategorisWithProductCount();

            return await Task.FromResult(View(categories));
        }
    }
}
