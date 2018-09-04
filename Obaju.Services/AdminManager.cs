using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Obaju.Data;
using Obaju.Models;
using Obaju.Models.BindingModels;
using Obaju.Models.ViewModels;
using Obaju.Services.Enums;
using Obaju.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Obaju.Services
{
    public class AdminManager : IAdminManager
    {
        private readonly ObajuDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmailSender _emailSender;

        private readonly string[] _colors = new string[]
        {
            "White", "Blue", "Green", "Yellow", "Red"
        };

        public AdminManager(ObajuDbContext db, IHostingEnvironment hostingEnvironment, IEmailSender emailSender)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            _emailSender = emailSender;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllCategoriesSelectListAsync()
        {
            var categoryGroups = new Dictionary<string, SelectListGroup>()
            {
                { "MenClothing", new SelectListGroup() { Name = "Men - Clothing" } },
                { "LadiesClothing", new SelectListGroup() { Name = "Ladies - Clothing" } },
                { "MenFootwear", new SelectListGroup() { Name = "Men - Footwear" } },
                { "LadiesFootwear", new SelectListGroup() { Name = "Ladies - Footwear" } }
            };

            return await _db.Categories.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString(),
                Group = categoryGroups[c.Gender + c.Type],
            })
            .ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetAllBrandsSelectListAsync()
        {
            return await _db.Brands.Select(b => new SelectListItem()
            {
                Text = b.Name,
                Value = b.Id.ToString()
            })
            .ToListAsync();
        }

        public IEnumerable<SelectListItem> GetAllColorsSelectList()
        {
            return _colors.Select(b => new SelectListItem()
            {
                Text = b,
                Value = b.ToLower()
            })
            .ToList();
        }

        // TODO: refactor
        public async Task<IEnumerable<ListIssuesViewModel>> GetAllIssuesAsync(IssueStatusFilter status)
        {
            if (status.HasFlag(IssueStatusFilter.All))
            {
                return await _db.Issues
                .Select(i => new ListIssuesViewModel()
                {
                    Id = i.Id,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Email = i.Email,
                    Subject = i.Subject,
                    IsOpen = i.IsOpen
                })
            .ToListAsync();
            }
            else if (status.HasFlag(IssueStatusFilter.OpenedOnly))
            {
                return await _db.Issues
               .Where(i => i.IsOpen == true)
               .Select(i => new ListIssuesViewModel()
               {
                   Id = i.Id,
                   FirstName = i.FirstName,
                   LastName = i.LastName,
                   Email = i.Email,
                   Subject = i.Subject,
                   IsOpen = i.IsOpen
               })
           .ToListAsync();
            }
            else if (status.HasFlag(IssueStatusFilter.ClosedOnly))
            {
                return await _db.Issues
               .Where(i => i.IsOpen == false)
               .Select(i => new ListIssuesViewModel()
               {
                   Id = i.Id,
                   FirstName = i.FirstName,
                   LastName = i.LastName,
                   Email = i.Email,
                   Subject = i.Subject,
                   IsOpen = i.IsOpen
               })
           .ToListAsync();
            }

            return new List<ListIssuesViewModel>();
        }

        public async Task CreateCategoryAsync(CreateCategoryBindingModel model)
        {
            await _db.Categories
                .AddAsync(new Category()
                {
                    Name = model.Name,
                    Gender = model.Gender,
                    Type = model.Group
                });

            await _db.SaveChangesAsync();
        }

        public async Task CreateBrandAsync(CreateBrandBindingModel model)
        {
            await _db.Brands
                .AddAsync(new Brand()
                {
                    Name = model.Name
                });

            await _db.SaveChangesAsync();
        }

        public async Task CreateProductAsync(CreateProductBindingModel model)
        {
            var imageList = new List<Image>();
            var webRootPath = Path.Combine(_hostingEnvironment.WebRootPath, "images");

            foreach (var image in model.Images)
            {
                if (image.Length > 0)
                {
                    var imageName = Regex.Replace(string.Format("{0}-{1}.png", model.Name, Guid.NewGuid()), @"\s+", "-");

                    using (var fileStream = new FileStream(Path.Combine(webRootPath, imageName), FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);

                        imageList.Add(new Image()
                        {
                            Path = Path.Combine("images", imageName)
                        });
                    }
                }
            }

            await _db.Products
                .AddAsync(new Product()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Details = model.Details,
                    Color = model.Color,
                    Quantity = 99,
                    CategoryId = model.CategoryId,
                    BrandId = model.BrandId,
                    Images = imageList,
                });

            await _db.SaveChangesAsync();
        }

        public async Task SendEmailToSubscribersAsync(SendNewsBindingModel model)
        {
            var subscribers = await _db
                .Subscribers
                .ToListAsync();

            foreach (var subscriber in _db.Subscribers)
            {
                await _emailSender.SendEmailAsync(subscriber.Email, model.Subject, model.Message);
            }
        }
    }
}
