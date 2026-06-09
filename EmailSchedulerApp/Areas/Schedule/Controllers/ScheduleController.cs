using Microsoft.AspNetCore.Mvc;
using EmailSchedulerApp.Services.Interfaces;

namespace EmailSchedulerApp.Controllers
{
    [Area("Schedule")]
    public class ScheduleController() : Controller
    {
        // private readonly IScheduleService _scheduleService = scheduleService;

        public async Task<IActionResult> Index()
        {
            // var data = await _scheduleService.GetScheduleDataAsync();

            return View();
        }

        public ActionResult CreateSchedule()
        {
            return View();
        }
    }
}