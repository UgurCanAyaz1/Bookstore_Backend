using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore_Backend.DAL.Context;
using Bookstore_Backend.DAL.Entities;
using Bookstore_Backend.Repository.Classes;

namespace Bookstore_Backend.Repositories.Classes
{
    public class BookRepository : GenericRepository<Book>
    {
        public BookRepository(BookstoreContext context) : base(context)
        {
        }
    }
}