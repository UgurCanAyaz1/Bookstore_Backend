using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore_Backend.DAL.Entities;
using Bookstore_Backend.Repositories.Interfaces;


namespace Bookstore_Backend.Services.Interfaces
{
    public interface IUserService: IGenericRepository<User>
    {
        public string QuickHash(string input);

    }
}