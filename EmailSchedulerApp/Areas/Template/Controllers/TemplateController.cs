using Microsoft.AspNetCore.Mvc;
using EmailSchedulerApp.Services.Interfaces;

namespace EmailSchedulerApp.Controllers
{
    [Area("Template")]
    public class TemplateController() : Controller
    {
        // private readonly ITemplateService _templateService = templateService;

        public async Task<IActionResult> Index()
        {
            // var data = await _templateService.GetScheduleDataAsync();

            return View();
        }
        
        public ActionResult CreateTemplate()
        {
            return View();
        }
    }
}