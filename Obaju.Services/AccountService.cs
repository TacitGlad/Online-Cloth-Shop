using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Obaju.Data;
using Obaju.Models;
using Obaju.Models.BindingModels;
using Obaju.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Obaju.Services
{
    public class AccountService : IAccountService
    {
        private readonly ObajuDbContext _db;
        private readonly UserManager<User> _userManager;

        public AccountService(ObajuDbContext db, UserManager<User> userManager)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<PersonalDetailsBindingModel> GetPersonalDetails(User user)
        {
            return await _db.Users
                .Where(u => u == user)
                .Select(u => new PersonalDetailsBindingModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    BillingAddress = u.BillingAddress,
                    BillingAddressLine2 = u.BillingAddressLine2,
                    Country = u.Country,
                    City = u.City,
                    PostalCode = u.PostalCode,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email
                })
                .FirstAsync();
        }

        public async Task UpdatePersonalDetails(User user, PersonalDetailsBindingModel model)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.BillingAddress = model.BillingAddress;
            user.BillingAddressLine2 = model.BillingAddressLine2;
            user.Country = model.Country;
            user.City = model.City;
            user.PostalCode = model.PostalCode;

            await _db.SaveChangesAsync();

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            var email = await _userManager.GetEmailAsync(user);
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }

                var setUserNameResult = await _userManager.SetUserNameAsync(user, model.Email);
                if (!setUserNameResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting name for user with ID '{userId}'.");
                }
            }
        }
    }
}
