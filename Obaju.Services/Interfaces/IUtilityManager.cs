using System.Collections.Generic;
using System.Threading.Tasks;
using Obaju.Models.BindingModels;

namespace Obaju.Services.Interfaces
{
    public interface IUtilityManager
    {
        Task AddSubscriberAsync(string email);

        List<string> GetCountryList();

        Task AddIssueAsync(ContactFormBindingModel contactForm);
    }
}
