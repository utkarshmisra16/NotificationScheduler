using EmailSchedulerApp.DTOs.Schedule;
using EmailSchedulerApp.Repositories.Interfaces;
using EmailSchedulerApp.Services.Interfaces;
using EmailSchedulerApp.Models;
using EmailSchedulerApp.DTOs.Template;
using System.Text.Json;

namespace EmailSchedulerApp.Services.Implementations
{
    public class ScheduleService(IScheduleRepository scheduleRepository, IScheduleDateService scheduleDateService) : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository = scheduleRepository;
        private readonly IScheduleDateService _scheduleDateService = scheduleDateService;

        public async Task<CreateScheduleResponseDto> SaveSchedule(CreateScheduleRequestDto request)
        {
            var schedule = new Schedule
            {
                TemplateId = request.TemplateId,
                Name = request.Name ?? string.Empty,
                Channel = request.Channel,
                Description = request.Description,
                Frequency = (int)request.Frequency,
                CronExpression = request.CronExpression,
                WeekDays = request.WeekDays != null ? string.Join(',', request.WeekDays) : null,
                StartTime = request.StartTime,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Timezone = request.Timezone,
                Tags = request.Tags,
                Priority = (int)request.Priority,
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                Recipients = JsonSerializer.Serialize(request.RecipientEmails),
            };
            schedule.NextRunAt = _scheduleDateService.CalculateFirstRun(schedule);
            if (!schedule.NextRunAt.HasValue)
            {
                return new CreateScheduleResponseDto
                {
                    Success = false,
                    Message = "Unable to calculate the first run time."
                };
            }
            int scheduleId = await _scheduleRepository.SaveSchedule(schedule);
            return new CreateScheduleResponseDto
            {
                ScheduleId = scheduleId,
                Success = true,
                Message = "Schedule created successfully."
            };
        }

        public async Task<List<TemplateDropdownDto>> GetTemplatesAsync()
        {
            return await _scheduleRepository.GetTemplatesAsync();
        }
    }
}