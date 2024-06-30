using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bookstore_Backend.DAL.Context;
using Bookstore_Backend.DAL.Entities;
using Bookstore_Backend.Repositories.Interfaces;
using Bookstore_Backend.Services.Interfaces;

namespace Bookstore_Backend.Services.Classes
{
    public class UserService : IUserService
    {
        public IGenericRepository<User> _repository;
        public BookstoreContext _context;

        public UserService(IGenericRepository<User> repository, BookstoreContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);

            await _context.SaveChangesAsync();
        }

        public async  Task<User> GetAsync(int id)
        {
            return await _repository.GetAsync(id);

        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task InsertAsync(User entity)
        {
            // Created user's role is set to "user" as default
            if (entity.Role=="admin"){
                entity.Role="admin";
            }
            else{
                entity.Role="user";
            }

            // Created user's password is hashed
            entity.PasswordHash=QuickHash(entity.PasswordHash);
            await _repository.InsertAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            await _repository.UpdateAsync(entity);

            await _context.SaveChangesAsync();
        }
        public string QuickHash(string input)
        {
			var inputBytes = Encoding.UTF8.GetBytes(input);
			var inputHash = SHA256.HashData(inputBytes);
			return Convert.ToHexString(inputHash);
        }    

    }
}