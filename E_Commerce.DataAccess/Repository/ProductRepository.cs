using E_Commerce.DataAccess.Data;
using E_Commerce.DataAccess.Repository.IRepository;
using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            _db.Update(product);
            var objFromDb = _db.Products.FirstOrDefault(s => s.Id == product.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = product.Title;
                objFromDb.Description = product.Description;
                objFromDb.Price = product.Price;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.Price = product.Price;
                objFromDb.Price50 = product.Price50;
                objFromDb.Price100 = product.Price100;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.ISBN = product.ISBN;
                objFromDb.Author = product.Author;
            }
        }

    }
}