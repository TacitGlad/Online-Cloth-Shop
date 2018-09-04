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

        public IList<CategoriesViewModel> GetCategoris()
        {
            return _db.Categories.
                Select(c => new CategoriesViewModel()
                {
                    Name = c.Name,
                    Gender = c.Gender,
                    Type = c.Type
                })
                .ToList();
        }

        public IList<CategoriesWithProductsCountViewModel> GetCategorisWithProductCount()
        {
            return _db.Categories.
                Select(c => new CategoriesWithProductsCountViewModel()
                {
                    Name = c.Name,
                    Gender = c.Gender,
                    ProductsCount = c.Products.Count()
                })
                .ToList();
        }

        public IList<BrandsViewModel> GetBrands()
        {
            return _db.Brands.
                Select(b => new BrandsViewModel()
                {
                    Name = b.Name,
                    Count = b.Products.Count()
                })
                .ToList();
        }

        public async Task<IList<ProductListViewModel>> GetProductList(string categoryGender, string categoryName)
        {
            IList<Product> products;

            if (categoryName == null)
            {
                products = await _db.Products
                    .Include(p => p.Images)
                    .Where(p => p.Category.Gender == categoryGender)
                    .ToListAsync();
            }
            else
            {
                products = await _db.Products
                    .Include(p => p.Images)
                    .Where(p => p.Category.Gender == categoryGender && p.Category.Name == categoryName)
                    .ToListAsync();
            }

            List<ProductListViewModel> productList = new List<ProductListViewModel>();

            foreach (var product in products)
            {
                var images = product.Images;

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
