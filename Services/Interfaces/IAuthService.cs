using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore_Backend.Models;

namespace Bookstore_Backend.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<UserLoginResponse> LoginUser(UserLoginRequest request);

        public string QuickHash(string input);
    }
}