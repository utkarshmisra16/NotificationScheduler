using Microsoft.AspNetCore.Mvc;
using EmailSchedulerApp.Services.Interfaces;

namespace EmailSchedulerApp.Controllers
{
    [Area("Dashboard")]
    public class DashboardController(IDashboardService dashboardService) : Controller
    {
        private readonly IDashboardService _dashboardService = dashboardService;

        public async Task<IActionResult> Index()
        {
            var data = await _dashboardService.GetDashboardAsync();

            return View(data);
        }
    }
}