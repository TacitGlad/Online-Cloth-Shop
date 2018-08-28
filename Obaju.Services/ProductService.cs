using Obaju.Data;
using Obaju.Models;
using Obaju.Models.ViewModels;
using Obaju.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Obaju.Services
{
    public class ProductService : IProductService
    {
        private readonly ObajuDbContext _db;

        public ProductService(ObajuDbContext db)
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

        public IList<ProductListViewModel> GetProductList(string categoryGender, string categoryName)
        {
            IQueryable<Product> queryProducts;

            if (string.IsNullOrEmpty(categoryName))
            {
                queryProducts = _db.Products.Where(p => p.Category.Gender == categoryGender);
            }
            else
            {
                queryProducts = _db.Products.Where(p => p.Category.Gender == categoryGender && p.Category.Name == categoryName);
            }
               
            List<ProductListViewModel> productList = new List<ProductListViewModel>();

            foreach (var product in queryProducts)
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
    }
}
