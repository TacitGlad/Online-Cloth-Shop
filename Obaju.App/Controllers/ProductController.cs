using Microsoft.AspNetCore.Mvc;
using Obaju.Services.Interfaces;
using System.Threading.Tasks;

namespace Obaju.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<IActionResult> Categories(string categoryGender, string categoryName)
        {
            ViewBag.CategoryGender = categoryGender;
            ViewBag.CategoryName = categoryName;

            var productList = await _productManager.GetProductList(categoryGender, categoryName);

            return View(productList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var productDetails = await _productManager.GetProductDetailsAsync(id);

            return View(productDetails);
        }
    }
}