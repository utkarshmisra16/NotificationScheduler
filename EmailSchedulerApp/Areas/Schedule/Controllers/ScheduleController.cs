using Microsoft.AspNetCore.Mvc;
using EmailSchedulerApp.Services.Interfaces;
using EmailSchedulerApp.DTOs.Schedule;
using EmailSchedulerApp.ViewModels.Schedule;
using Microsoft.AspNetCore.Authorization;

namespace EmailSchedulerApp.Controllers
{
    [Area("Schedule")]
    [Authorize]
    public class ScheduleController(IScheduleService scheduleService) : Controller
    {
        private readonly IScheduleService _scheduleService = scheduleService;

        [HttpGet]
        public async Task<ActionResult> CreateScheduleAsync()
        {
            var vm = new CreateScheduleViewModel
            {
                Templates = await _scheduleService.GetTemplatesAsync()
            };
            return View(vm);
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

        public IActionResult ViewSchedules()
        {
            return View();
        }
    }
}