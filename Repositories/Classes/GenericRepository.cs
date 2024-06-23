using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookstore_Backend.DAL.Context;
using Bookstore_Backend.Repositories.Interfaces;

namespace Bookstore_Backend.Repository.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly BookstoreContext _context;

        public GenericRepository(BookstoreContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
        }
    }
}
