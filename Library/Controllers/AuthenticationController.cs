using Library.DTO.Authentication;
using Library.Models;
using Library.Services.AuthenticationService;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO, string returnUrl = "/home/index")
        {
            ServiceResponse response = new ServiceResponse();
            if (ModelState.IsValid)
            {
                response = await _authenticationService.Register(userRegisterDTO.RegisterUsername, userRegisterDTO.RegisterPassword) as ServiceResponse;
                if (!response.Success)
                    return BadRequest(response);
                else
                    return await Login(new UserLoginDTO { LoginUsername = userRegisterDTO.RegisterUsername, LoginPassword = userRegisterDTO.RegisterPassword }, returnUrl);
            }
            else
            {
                response.Message = "Invalid data";
                response.Success = false;
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO, string returnUrl = "/home/index")
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (ModelState.IsValid)
            {
                response = await _authenticationService.Login(userLoginDTO.LoginUsername, userLoginDTO.LoginPassword) as ServiceResponse<string>;
                if (!response.Success)
                    return BadRequest(response);
                response.Data = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value + returnUrl;
                return Ok(response);

            } else
            {
                response.Message = "Invalid data";
                response.Success = false;
                return BadRequest(response);
            }
        }

        public IActionResult Logout()
        {
            ServiceResponse response = _authenticationService.Logout() as ServiceResponse;
            if(!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
