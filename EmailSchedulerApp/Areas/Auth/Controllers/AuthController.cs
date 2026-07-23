using EmailSchedulerApp.DTOs;
using EmailSchedulerApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginRequestDto request)
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
            return Json(response);
        }
    }
}