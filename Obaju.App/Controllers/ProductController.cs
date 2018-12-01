using Microsoft.AspNetCore.Mvc;
using Obaju.Models.ViewModels;
using Obaju.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IActionResult> Categories(string gender, string category, Dictionary<string, bool> brandsFilter)
        {
            ViewBag.Gender = gender;
            ViewBag.Category = category;

            var productList = await _productManager.GetProductList(gender, category, brandsFilter.Keys.ToList());

            return View(productList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var productDetails = await _productManager.GetProductDetailsAsync(id);

            return View(productDetails);
        }
    }
}