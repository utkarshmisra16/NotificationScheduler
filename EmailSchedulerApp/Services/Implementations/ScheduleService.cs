using EmailSchedulerApp.DTOs.Schedule;
using EmailSchedulerApp.Repositories.Interfaces;
using EmailSchedulerApp.Services.Interfaces;
using EmailSchedulerApp.Models;
using EmailSchedulerApp.DTOs.Template;

namespace EmailSchedulerApp.Services.Implementations
{
    public class ScheduleService(IScheduleRepository scheduleRepository) : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository = scheduleRepository;

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
                CreatedOn = DateTime.UtcNow
            };
            int scheduleId = await _scheduleRepository.SaveSchedule(schedule);

            // var recipients = request.CustomEmails?
            //     .Split([',', '\n', '\r'], StringSplitOptions.RemoveEmptyEntries)
            //     .Select(email => new Recipient
            //     {
            //         ScheduleId = scheduleId,
            //         Name = null,
            //         Email = email.Trim(),
            //         Source = "MANUAL",
            //         AddedAt = DateTime.UtcNow
            //     })
            //     .ToList();

            // if (recipients != null && recipients.Any())
            // {
            //     await _scheduleRepository.SaveRecipients(recipients);
            // }

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