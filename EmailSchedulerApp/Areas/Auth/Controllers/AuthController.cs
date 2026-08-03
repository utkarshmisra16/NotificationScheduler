using EmailSchedulerApp.DTOs;
using EmailSchedulerApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EmailSchedulerApp.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return PartialView("_Login");;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return PartialView("_Register");;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new LoginResponsedto
                {
                    Success = false,
                    Message = "Please enter email and password."
                });
            }

            var response = _authService.Login(request);

            if (!response.Success)
            {
                return Json(response);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.Email)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return Json(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync( CookieAuthenticationDefaults.AuthenticationScheme );
            return Json(new { Success = true });
        }
    }
}