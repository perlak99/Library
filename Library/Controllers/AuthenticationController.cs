using Library.DTO;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class AuthenticationController : Controller
    {
        readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            ServiceResponse response = await _authenticationService.Register(userRegisterDTO.RegisterUsername, userRegisterDTO.RegisterPassword);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            ServiceResponse response = await _authenticationService.Login(userLoginDTO.LoginUsername, userLoginDTO.LoginPassword);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
