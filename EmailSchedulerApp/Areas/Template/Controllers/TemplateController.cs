using Microsoft.AspNetCore.Mvc;
using EmailSchedulerApp.Services.Interfaces;
using EmailSchedulerApp.DTOs.Template;
using Microsoft.AspNetCore.Authorization;

namespace EmailSchedulerApp.Controllers
{
    [Area("Template")]
    [Authorize]
    public class TemplateController(ITemplateService templateService) : Controller
    {
        private readonly ITemplateService _templateService = templateService;
        
        public async Task<IActionResult> Index()
        {
            string hash = BCrypt.Net.BCrypt.HashPassword("1234");
            Console.WriteLine(hash);
            return View();
        }

        [HttpGet]
        public ActionResult CreateTemplate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTemplate(CreateTemplateRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new CreateTemplateResponseDto
                {
                    Success = false,
                    Message = "Validation failed."
                });
            }

            CreateTemplateResponseDto response =
                await _templateService.SaveTemplate(request);

            return Json(response);
        }

        public IActionResult ViewTemplates()
        {
            return View();
        }

    }
}