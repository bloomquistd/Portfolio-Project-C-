using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.DataAccess.Repository.IRepository;
using E_Commerce.DataAccess.Data;

namespace E_Commerce.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
     {
          private readonly ApplicationDbContext _db;
          public ICategoryRepository Category { get; private set; }
          public IProductRepository Product { get; private set; }
          public ICompanyRepository Company { get; private set; }
          public IShoppingCartRepository ShoppingCart { get; private set; }
          public IApplicationUserRepository ApplicationUser { get; private set; }
          public IOrderHeaderRepository OrderHeader { get; private set; }
          public IOrderDetailRepository OrderDetail { get; private set; }
          public IProductImageRepository ProductImage { get; private set; }
    
          public UnitOfWork(ApplicationDbContext db)
          {
                _db = db;
                Category = new CategoryRepository(_db);
                Product = new ProductRepository(_db);
                Company = new CompanyRepository(_db);
                ShoppingCart = new ShoppingCartRepository(_db);
                ApplicationUser = new ApplicationUserRepository(_db);
                OrderHeader = new OrderHeaderRepository(_db);
                OrderDetail = new OrderDetailRepository(_db);
                ProductImage = new ProductImageRepository(_db);
          }
    

    
          public void Save()
          {
                _db.SaveChanges();
          }
     }
}