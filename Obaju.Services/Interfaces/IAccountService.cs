using Obaju.Models;
using Obaju.Models.BindingModels;
using System.Threading.Tasks;

namespace Obaju.Services.Interfaces
{
    public interface IAccountService
    {
        Task<PersonalDetailsBindingModel> GetPersonalDetails(User user);

        Task UpdatePersonalDetails(User user, PersonalDetailsBindingModel model);
    }
}
