using EmailSchedulerApp.DTOs.Schedule;
namespace EmailSchedulerApp.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<CreateScheduleResponseDto> SaveSchedule(CreateScheduleRequestDto request);
    }
}