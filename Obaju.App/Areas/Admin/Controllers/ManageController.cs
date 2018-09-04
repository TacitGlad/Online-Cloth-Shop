using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obaju.Constants;
using Obaju.Models.BindingModels;
using Obaju.Services.Enums;
using Obaju.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Obaju.App.Areas.Admin.Controllers
{
    [Area(RoleName.Admin)]
    [Authorize(Roles = RoleName.Admin)]
    public class ManageController : Controller
    {
        private readonly IAdminManager _adminManager;

        public ManageController(IAdminManager adminManager)
        {
            _adminManager = adminManager;
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryBindingModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            await _adminManager.CreateCategoryAsync(model);
            TempData["Success"] = "You successfully created new category.";
            return RedirectToAction("CreateCategory");
        }

        [HttpGet]
        public IActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _adminManager.CreateBrandAsync(model);
            TempData["Success"] = "You successfully created new brand.";
            return RedirectToAction("CreateBrand");
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Categories = await _adminManager.GetAllCategoriesSelectListAsync();
            ViewBag.Brands = await _adminManager.GetAllBrandsSelectListAsync();
            ViewBag.Colors = _adminManager.GetAllColorsSelectList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach(var image in model.Images)
            {
                if (image.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageTypeMismatch", "One of your images extension is incorrect.");
                    return View();
                }
            }

            await _adminManager.CreateProductAsync(model);
            TempData["Success"] = "You successfully created new product.";
            return RedirectToAction("CreateProduct");
        }

        [HttpGet]
        public async Task<IActionResult> Issues(string opened)
        {
            var viewModel = await _adminManager.GetAllIssuesAsync(IssueStatusFilter.All);
            return View(model:viewModel);
        }

        [HttpGet]
        public IActionResult SendNewspaper()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendNewspaper(SendNewsBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _adminManager.SendEmailToSubscribersAsync(model);

            TempData["Success"] = "You successfully sent an email to all of our subscribers.";

            return RedirectToAction("SendNewspaper");
        }
    }
}