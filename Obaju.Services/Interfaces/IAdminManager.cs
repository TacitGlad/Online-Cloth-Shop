using Microsoft.AspNetCore.Mvc.Rendering;
using Obaju.Models.BindingModels;
using Obaju.Models.ViewModels;
using Obaju.Services.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Obaju.Services.Interfaces
{
    public interface IAdminManager
    {
        Task<IEnumerable<SelectListItem>> GetAllCategoriesSelectListAsync();

        Task<IEnumerable<SelectListItem>> GetAllBrandsSelectListAsync();

        IEnumerable<SelectListItem> GetAllColorsSelectList();

        Task<IEnumerable<ListIssuesViewModel>> GetAllIssuesAsync(IssueStatusFilter status);

        Task CreateCategoryAsync(CreateCategoryBindingModel model);

        Task CreateBrandAsync(CreateBrandBindingModel model);

        Task CreateProductAsync(CreateProductBindingModel model);

        Task SendEmailToSubscribersAsync(SendNewsBindingModel model);
    }
}
