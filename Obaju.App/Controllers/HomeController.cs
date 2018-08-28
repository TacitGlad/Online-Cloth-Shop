using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Obaju.Models.BindingModels;
using Obaju.Services.Interfaces;
using System.Threading.Tasks;

namespace Obaju.App.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUtilityManager _utilityManager;

        public HomeController(ILogger<HomeController> logger, IUtilityManager utilityManager)
        {
            _logger = logger;
            _utilityManager = utilityManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscribeBindingModel model)
        {
            _logger.LogInformation($"{model.Email} subscribed for us.");

            await _utilityManager.AddSubscriberAsync(model.Email);

            return Json("You successfully subscribed for our newspaper");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if(statusCode.HasValue)
            {
                if(statusCode.Value == 404)
                {
                    var viewName = statusCode.ToString();
                    return View(viewName: viewName);
                }
            }

            return View();
        }
    }
}
