using Ecom.Core.Interfaces;
using Ecom.Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastracture.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public  AppDbContext Context { get; }
        public GenericRepository(AppDbContext _context)
        {
            Context = _context;
        }


        public async Task AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var entity = await Context.Set<T>().FindAsync(Id);
             Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await Context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
           var query=Context.Set<T>().AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);

            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await Context.Set<T>().FindAsync(Id); 
        }

        public async Task<T> GetByIdAsync(int Id, params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            var query = Context.Set<T>().AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);

            }
            var entity = await query.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == Id);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
           Context.Entry(entity).State=EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
         return   await Context.Set<T>().CountAsync();
        }
    }
}
