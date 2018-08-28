using Microsoft.AspNetCore.Mvc;
using Obaju.Models.BindingModels;
using System.Threading.Tasks;

namespace Obaju.App.Areas.Identity.Components
{
    public class ChangePasswordViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(new ChangePasswordBindingModel()));
        }
    }
}
