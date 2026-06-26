using Microsoft.AspNetCore.Mvc;
using EmailSchedulerApp.Services.Interfaces;
using EmailSchedulerApp.DTOs.Schedule;

namespace EmailSchedulerApp.Controllers
{
    [Area("Schedule")]
    public class ScheduleController(IScheduleService scheduleService) : Controller
    {
        private readonly IScheduleService _scheduleService = scheduleService;

        [HttpGet]
        public ActionResult CreateSchedule()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { Field = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage) }).ToList();
                return Json(new CreateScheduleResponseDto{
                    Success = false,
                    Message = "Validation failed."
                });
            }
            CreateScheduleResponseDto response = await _scheduleService.SaveSchedule(request);
            return Json(response);
        }
    }
}