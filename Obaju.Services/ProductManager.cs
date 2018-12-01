using Microsoft.EntityFrameworkCore;
using Obaju.Data;
using Obaju.Models;
using Obaju.Models.ViewModels;
using Obaju.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Obaju.Services
{
    public class ProductManager : IProductManager
    {
        private readonly ObajuDbContext _db;

        public ProductManager(ObajuDbContext db)
        {
            _db = db;
        }

        public Task<List<NavCategoriesViewModel>> GetNavCategorisAsync()
        {
            return _db.Categories.
                Select(c => new NavCategoriesViewModel()
                {
                    Name = c.Name,
                    Gender = c.Gender,
                    Type = c.Type
                })
                .ToListAsync();
        }

        public Task<List<PanelCategoriesViewModel>> GetPanelCategorisAsync()
        {
            return _db.Categories.
                Select(c => new PanelCategoriesViewModel()
                {
                    Name = c.Name,
                    Gender = c.Gender,
                    ProductsCount = c.Products.Count()
                })
                .ToListAsync();
        }

        public Task<List<BrandsViewModel>> GetBrandsAsync()
        {
            return _db.Brands.
                Select(b => new BrandsViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Count = b.Products.Count
                })
                .ToListAsync();
        }

        public async Task<IList<ProductListViewModel>> GetProductList(string gender, string category, IList<string> brandsFilter)
        {
            IList<Product> products;

            if (category == null)
            {
                products = await _db.Products
                    .Include(p => p.Images)
                    .Where(p => p.Category.Gender == gender)
                    .ToListAsync();
            }
            else
            {
                products = await _db.Products
                    .Include(p => p.Images)
                    .Where(p => p.Category.Gender == gender && p.Category.Name == category)
                    .ToListAsync();
            }

            IList<ProductListViewModel> productList = new List<ProductListViewModel>();

            foreach (var product in products)
            {
                if(!brandsFilter.Contains(product.Brand.Name))
                {
                    continue;
                }

                var images = product.Images;

                // TODO: add default image if null
                var frontImage = images.FirstOrDefault().Path;
                var backImage = images.Skip(1).FirstOrDefault().Path;

                productList.Add(new ProductListViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    FrontImage = frontImage,
                    BackImage = backImage,
                });
            }

            return productList;
        }

        public async Task<ProductDetailsViewModel> GetProductDetailsAsync(int id)
        {
            return await _db.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailsViewModel()
                {
                    Name = p.Name,
                    Details = Regex.Replace(p.Details, "\r\n", @"<br \>"),
                    Price = p.Price,
                    Images = p.Images.Select(i => i.Path).ToList()
                })
                .SingleOrDefaultAsync();
        }
    }
}
