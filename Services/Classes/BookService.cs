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
        public void Delete(int id)
        {
            _repository.Delete(id);

            _context.SaveChanges();
        }

        public Book Get(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(Book entity)
        {
            _repository.Insert(entity);

            _context.SaveChanges();
        }

        public void Update(Book entity)
        {
            _repository.Update(entity);

            _context.SaveChanges();
        }
    }
}