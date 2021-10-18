using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IAuthenticationService
    {
        public Task<ServiceResponse> Register(string username, string password);
        public Task<ServiceResponse> Login(string username, string password);
    }
}
