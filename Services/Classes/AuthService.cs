using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

		public string QuickHash(string input)
		{
			var inputBytes = Encoding.UTF8.GetBytes(input);
			var inputHash = SHA256.HashData(inputBytes);
			return Convert.ToHexString(inputHash);
		}



        private bool ValidatePassword(string password, string storedHash)
        {
            string hashedPassword = QuickHash(password);
            bool comparison;
            if (hashedPassword == storedHash){
                comparison = true;
            }
            else
            {
                comparison = false;
            }

            return comparison;
        }

        public async Task<UserLoginResponse> LoginUser(UserLoginRequest request)
        {
            UserLoginResponse response = new();

            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentNullException(nameof(request), "Username or password is null or empty.");
            }

            var users = await _userService.GetAllAsync();

            var user = users.FirstOrDefault(u => u.UserName == request.Username);

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

            if (request.Username==user.UserName && QuickHash(request.Password)==user.PasswordHash)
            {
                var generateTokenResult = await _tokenService.GenerateToken(new GenerateTokenRequest { Username = request.Username }, user);
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