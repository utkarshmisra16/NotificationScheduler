using Microsoft.AspNetCore.Mvc;
using EmailSchedulerApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EmailSchedulerApp.Areas.Logs.Controllers
{
    [Area("Logs")]
    [Authorize]
    public class LogsController : Controller
    {
        // private readonly IProfileService _profileService = profileService;

        public async Task<IActionResult> Index()
        {
            // var data = await _profileService.GetProfileAsync();
            return View();
        }
    }
}