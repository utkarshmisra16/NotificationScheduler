using EmailSchedulerApp.DTOs.Schedule;
using EmailSchedulerApp.DTOs.Template;
namespace EmailSchedulerApp.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<CreateScheduleResponseDto> SaveSchedule(CreateScheduleRequestDto request);
        Task<List<TemplateDropdownDto>> GetTemplatesAsync();
    }
}