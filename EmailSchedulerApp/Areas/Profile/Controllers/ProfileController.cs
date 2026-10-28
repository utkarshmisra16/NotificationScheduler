using Microsoft.AspNetCore.Mvc;
using EmailSchedulerApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EmailSchedulerApp.Controllers
{
    [Area("Profile")]
    [Authorize]
    public class ProfileController : Controller
    {
        // private readonly IProfileService _profileService = profileService;

        public async Task<IActionResult> Index()
        {
            Console.WriteLine("========== PROFILE INDEX HIT ==========");
            // var data = await _profileService.GetProfileAsync();
            Console.WriteLine("========== PROFILE DATA LOADED ==========");
            return View();
        }
    }
}