using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Obaju.Models;
using Obaju.Models.BindingModels;
using Obaju.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Obaju.App.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUtilityManager _utilityManager;
        private readonly IAccountManager _accountService;
        
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger, IEmailSender emailSender, IUtilityManager utilityManager, IAccountManager accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _utilityManager = utilityManager;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult MyOrders()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MyWishlist()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PersonalDetails()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            ViewBag.CountryList = _utilityManager.GetCountryList()
                .Select(c => new SelectListItem { Value = c, Text = c })
                .ToList();

            var viewModel = await _accountService.GetPersonalDetails(user);

            return View(model: viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PersonalDetails(PersonalDetailsBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await _accountService.UpdatePersonalDetails(user, model);

            await _signInManager.RefreshSignInAsync(user);
            TempData.Add("Success", "Your profile has been updated");
            return RedirectToAction("MyAccount");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return View();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            TempData["Success"] = "Your password has been changed.";

            return RedirectToAction("ChangePassword");
        }
    }
}