using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore_Backend.DAL.Entities;
using Bookstore_Backend.Models;
using Bookstore_Backend.Services.Interfaces;

namespace Bookstore_Backend.Services.Classes
{
    public class AuthService : IAuthService
    {
        public IUserService _userService;

        public ITokenService _tokenService;
        public AuthService(IUserService userService, ITokenService tokenService)
        {
            _userService=userService;
            _tokenService=tokenService;
        }



        private bool ValidatePassword(string password, string storedHash)
        {
            // Implement your password hash validation logic here
            // For example, using BCrypt:
            // return BCrypt.Net.BCrypt.Verify(password, storedHash);
            return password == storedHash; // Replace with actual hash comparison
        }

        public UserLoginResponse LoginUser(UserLoginRequest request)
        {
            UserLoginResponse response = new();

            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentNullException(nameof(request), "Username or password is null or empty.");
            }

            var user = _userService.GetAll().FirstOrDefault(u => u.UserName == request.Username);

            if (user == null){

                return new UserLoginResponse
                {
                    AuthenticateResult = false,
                    AuthToken = string.Empty,
                    AccessTokenExpireDate = DateTime.UtcNow
                };
            }

            // Entered password's hash and stored password's hash are not matching
            if (!ValidatePassword(request.Password, user.PasswordHash))
            {
                return new UserLoginResponse
                {
                    AuthenticateResult = false,
                    AuthToken = string.Empty,
                    AccessTokenExpireDate = DateTime.UtcNow
                };
            }

            if (request.Username==user.UserName && request.Password==user.PasswordHash)
            {
                var generateTokenResult = _tokenService.GenerateToken(new GenerateTokenRequest { Username = request.Username }, user);
                response = new UserLoginResponse
                {
                    AuthToken = generateTokenResult.Token,
                    AuthenticateResult = true,
                    AccessTokenExpireDate = generateTokenResult.TokenExpireDate
                };
            }            

            return response;            
        }
    }
}