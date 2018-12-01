using Microsoft.AspNetCore.Mvc;
using Obaju.Services.Interfaces;
using System.Threading.Tasks;

namespace Obaju.App.ViewComponents
{
    public class BrandsPanelViewComponent : ViewComponent
    {
        public readonly IProductManager _productManager;

        public BrandsPanelViewComponent(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string categoryGender, string categoryName)
        {
            var viewModel = await _productManager
                .GetBrandsAsync();

            return View(viewModel);
        }
    }
}
