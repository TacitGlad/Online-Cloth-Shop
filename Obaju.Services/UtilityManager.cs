using Microsoft.EntityFrameworkCore;
using Obaju.Data;
using Obaju.Models;
using Obaju.Models.BindingModels;
using Obaju.Services.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Obaju.Services
{
    public class UtilityManager : IUtilityManager
    {
        private readonly ObajuDbContext _db;

        public UtilityManager(ObajuDbContext db)
        {
            _db = db;
        }

        public async Task AddSubscriberAsync(string email)
        {
            var subscriber = await _db.Subscribers.FirstOrDefaultAsync(s => s.Email == email);

            if (subscriber == null)
            {
                await _db.Subscribers.AddAsync(new Subscriber()
                {
                    Email = email
                });

                await _db.SaveChangesAsync();
            }
        }

        public List<string> GetCountryList()
        {
            List<string> cultureList = new List<string>();

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.LCID);

                if (!(cultureList.Contains(region.EnglishName)))
                {
                    cultureList.Add(region.EnglishName);
                }
            }

            cultureList.Sort();

            return cultureList;
        }

        public async Task AddIssueAsync(ContactFormBindingModel contactForm)
        {
            await _db.Issues.AddAsync(new Issues()
            {
                FirstName = contactForm.FirstName,
                LastName = contactForm.LastName,
                Email = contactForm.Email,
                Subject = contactForm.Subject,
                Message = contactForm.Message
            });

            await _db.SaveChangesAsync();
        }
    }
}
