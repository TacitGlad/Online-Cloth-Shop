using Microsoft.AspNetCore.Mvc;
using Obaju.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Obaju.App.ViewComponents
{
    public class BrandsPanelViewComponent : ViewComponent
    {
        public readonly IAdminManager _adminManager;

        public BrandsPanelViewComponent(IAdminManager adminManager)
        {
            _adminManager = adminManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = await _adminManager
                .GetAllBrandsSelectListAsync();

            return View(viewModel.Select(b => b.Text));
        }
    }
}
