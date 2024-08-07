using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore_Backend.DAL.Context;
using Bookstore_Backend.DAL.Entities;
using Bookstore_Backend.Repositories.Interfaces;
using Bookstore_Backend.Services.Interfaces;

namespace Bookstore_Backend.Services.Classes
{
    public class BookService : IBookService
    {
        public IGenericRepository<Book> _repository;
        public BookstoreContext _context;

        public BookService(IGenericRepository<Book> repository, BookstoreContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);

            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task InsertAsync(Book entity)
        {
            await _repository.InsertAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book entity)
        {
            await _repository.UpdateAsync(entity);

            await _context.SaveChangesAsync();
        }
    }
}