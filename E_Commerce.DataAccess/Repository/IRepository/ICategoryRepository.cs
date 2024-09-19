using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Models;

namespace E_Commerce.DataAccess.Repository.IRepository
{
   public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}