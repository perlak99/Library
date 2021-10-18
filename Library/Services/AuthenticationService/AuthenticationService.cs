using Library.DAL;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class AuthenticationService : IAuthenticationService 
    {
        readonly LibraryContext _libraryContext;

        public AuthenticationService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<ServiceResponse> Login(string username, string password)
        {
            ServiceResponse response = new ServiceResponse();
            User user = await _libraryContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong username or password";
            } 
            else
            {
                response.Success = true;
                response.Message = "Successfully logged in";
            }

            return response;
        }


        public async Task<ServiceResponse> Register(string username, string password)
        {
            ServiceResponse response = new ServiceResponse();

            if (await UserExists(username))
            {
                response.Success = false;
                response.Message = "User already exists";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _libraryContext.Users.AddAsync(user);
            await _libraryContext.SaveChangesAsync();

            response.Success = true;
            response.Message = "Successfully registered user";
            return response;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _libraryContext.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for(int i=0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != passwordHash[i])
                    return false;
            }
            return true;
        }
    }
}
