using EmailSchedulerApp.DTOs.Schedule;
using EmailSchedulerApp.DTOs.Template;

namespace EmailSchedulerApp.ViewModels.Schedule
{
    public class CreateScheduleViewModel
    {
        public CreateScheduleRequestDto Schedule { get; set; } = new();
        public List<TemplateDropdownDto> Templates { get; set; } = [];
    }
}