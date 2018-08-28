using Microsoft.AspNetCore.Mvc;
using Obaju.Services.Interfaces;
using System.Threading.Tasks;

namespace Obaju.App.ViewComponents
{
    public class CategoryNavigationViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public CategoryNavigationViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           var categories = _productService.GetCategoris();

            return await Task.FromResult(View(categories));
        }
    }
}
