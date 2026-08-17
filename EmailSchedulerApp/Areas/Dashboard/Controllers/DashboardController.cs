using Microsoft.AspNetCore.Mvc;
using EmailSchedulerApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EmailSchedulerApp.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class DashboardController(IDashboardService dashboardService) : Controller
    {
        private readonly IDashboardService _dashboardService = dashboardService;

        public async Task<IActionResult> Index()
        {
            Console.WriteLine("========== DASHBOARD INDEX HIT ==========");
            var data = await _dashboardService.GetDashboardAsync();
            Console.WriteLine("========== DASHBOARD DATA LOADED ==========");
            return View(data);
        }
    }
}