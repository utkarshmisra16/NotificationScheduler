using System.Text.Json;
using EmailSchedulerApp.Enums;
using EmailSchedulerApp.Repositories.Interfaces;
using EmailSchedulerApp.Services.Interfaces;
using EmailSchedulerApp.ViewModels.Dashboard;

namespace EmailSchedulerApp.Services.Implementation
{
    public class DashboardService(IDashboardRepository repository) : IDashboardService
    {
        private readonly IDashboardRepository _repository = repository;

        public async Task<DashboardViewModel> GetDashboardAsync()
        {
            var recentSchedules = await _repository.GetRecentSchedulesAsync();

            return new DashboardViewModel
            {
                RecentEmails = recentSchedules
                    .Select(x =>
                    {
                        List<string> recipients = [];

                        if (!string.IsNullOrWhiteSpace(x.Recipients))
                        {
                            try
                            {
                                recipients = JsonSerializer.Deserialize<List<string>>(x.Recipients) ?? [];
                            }
                            catch (JsonException)
                            {
                                recipients = [];
                            }
                        }

                        return new RecentEmailViewModel
                        {
                            ScheduleId = x.ScheduleId,
                            Subject = x.Name,

                            // Recipient count
                            Recipients = x.RecipientCount,

                            // Recipient email list for Bootstrap Popover
                            RecipientList = string.Join(
                                "",
                                recipients.Select(email =>
                                    $"<div class='mb-1'>" +
                                    $"<i class='bi bi-envelope me-2'></i>{email}" +
                                    $"</div>")
                            ),

                            ScheduledTime =
                                $"{x.StartDate:dd MMM yyyy} {x.StartTime}",

                            Status = x.IsActive
                                ? EmailStatus.Pending
                                : EmailStatus.Sent
                        };
                    })
                    .ToList()
            };
        }
    }
}