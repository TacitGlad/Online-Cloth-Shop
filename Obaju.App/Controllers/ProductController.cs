using Microsoft.AspNetCore.Mvc;
using Obaju.Services.Interfaces;

namespace Obaju.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Categories(string categoryGender, string categoryName)
        {
            ViewBag.CategoryGender = categoryGender;
            ViewBag.CategoryName = categoryName;

            var productList = _productService.GetProductList(categoryGender, categoryName);

            return View(productList);
        }
    }
}