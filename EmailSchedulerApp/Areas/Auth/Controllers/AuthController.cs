using EmailSchedulerApp.DTOs;
using EmailSchedulerApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using EmailSchedulerApp.ViewModels;

namespace EmailSchedulerApp.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpGet]
        public IActionResult Index() => View("index");

        [HttpGet]
        public IActionResult Login() => PartialView("_Login");

        [HttpGet]
        public IActionResult Register() => PartialView("_Register");

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return Json(new LoginResponsedto { Success = false, Message = "Please enter email and password." });
            var response = _authService.Login(request);
            if (!response.Success)
                return Json(response);
            var claims = new List<Claim>
                { new(ClaimTypes.Name, request.Email ?? string.Empty) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = request.RememberMe
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
            return Json(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
                return Json(new LoginResponsedto { Success = false, Message = "Please enter all required fields correctly." });
            var response = _authService.Register(request);
            return Json(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Json(new { Success = true });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Please enter a valid email address." });

            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            bool result = await _authService.ForgotPassword(request.Email, baseUrl);

            if (!result)
                return BadRequest(new { success = false, message = "Unable to process password reset request." });

            return Ok(new { success = true, message = "If an account exists with this email, a password reset link has been sent." });
        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest("Invalid reset link.");
            }

            var model = new ResetPasswordViewModel
            {
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool result = _authService.ResetPassword(
                model.Token,
                model.NewPassword
            );

            if (!result)
            {
                ModelState.AddModelError(
                    "",
                    "The reset link is invalid or has expired."
                );

                return View(model);
            }

            return RedirectToAction("Login", "Auth");
        }
    }
}