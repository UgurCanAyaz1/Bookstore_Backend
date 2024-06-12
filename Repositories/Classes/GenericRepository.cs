using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore_Backend.DAL;
using Bookstore_Backend.DAL.Context;
using Bookstore_Backend.Repositories.Interfaces;

namespace Bookstore_Backend.Repository.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private BookstoreContext _context;

        public GenericRepository(BookstoreContext context)
        {
            _context=context;
        }
        public void Delete(int id)
        {
            _context.Remove<TEntity>(Get(id));
			// _context.SaveChanges();
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public void Insert(TEntity entity)
        {
            _context.Add<TEntity>(entity);
			// _context.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            _context.Update<TEntity>(entity);
			// _context.SaveChanges();
        }
    }
}
