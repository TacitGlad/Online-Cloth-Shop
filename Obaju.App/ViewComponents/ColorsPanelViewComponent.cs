using Microsoft.AspNetCore.Mvc;
using Obaju.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Obaju.App.ViewComponents
{
    public class ColorsPanelViewComponent : ViewComponent
    {
        public readonly IAdminManager _adminManager;

        public ColorsPanelViewComponent(IAdminManager adminManager)
        {
            _adminManager = adminManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = _adminManager
                .GetAllColorsSelectList();

            return await Task.FromResult(View(viewModel.Select(b => b.Text)));
        }
    }
}
