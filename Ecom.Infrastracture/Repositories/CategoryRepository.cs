using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastracture.Repositories
{
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext _context):base(_context)
        {
            Context = _context;
        }

        public AppDbContext Context { get; }
    }
}
