using Microsoft.AspNetCore.Mvc;
using Obaju.Services.Interfaces;
using System.Threading.Tasks;

namespace Obaju.App.ViewComponents
{
    public class CategoriesNavViewComponent : ViewComponent
    {
        private readonly IProductManager _productManager;

        public CategoriesNavViewComponent(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           var categories = await _productManager
                .GetNavCategorisAsync();

            return View(categories);
        }
    }
}
