using Library.DAL;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService 
    {
        readonly LibraryContext _libraryContext;
        readonly IConfiguration _configuration;
        readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(LibraryContext libraryContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _libraryContext = libraryContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IServiceResponse> Login(string username, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User user = await _libraryContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong username or password";
            } 
            else
            {
                _httpContextAccessor.HttpContext.Session.SetString("Token", CreateToken(user));
                response.Message = "Successfully logged in";
            }

            return response;
        }


        public async Task<IServiceResponse> Register(string username, string password)
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

            response.Message = "Successfully registered user";
            return response;
        }

        public IServiceResponse Logout()
        {
            ServiceResponse response = new ServiceResponse();
            _httpContextAccessor.HttpContext.Session.Remove("Token");
            response.Message = "Successfully logged out";
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

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
