using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        public Task<IServiceResponse> Register(string username, string password);
        public Task<IServiceResponse> Login(string username, string password);
    }
}
