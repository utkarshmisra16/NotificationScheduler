using EmailSchedulerApp.Enums;
using EmailSchedulerApp.Models;
using EmailSchedulerApp.Services.Interfaces;

namespace EmailSchedulerApp.Services.Implementations
{
    public class ScheduleDateService : IScheduleDateService
    {
        public DateTime? CalculateFirstRun(Schedule schedule)
        {
            DateTime startDateTime = schedule.StartDate.Date.Add(schedule.StartTime);
            FrequencyType frequency = (FrequencyType)schedule.Frequency;

            DateTime? nextRun = frequency switch
            {
                FrequencyType.Once => startDateTime,
                FrequencyType.Daily => startDateTime,
                FrequencyType.Weekly => CalculateWeeklyRun(schedule, startDateTime),
                FrequencyType.Monthly => startDateTime,
                FrequencyType.Yearly => startDateTime,
                FrequencyType.Custom => null,
                _ => null
            };
            return ValidateEndDate(schedule, nextRun);
        }

        public DateTime? CalculateNextRun(Schedule schedule)
        {
            if (!schedule.NextRunAt.HasValue)
                return null;
            FrequencyType frequency = (FrequencyType)schedule.Frequency;
            DateTime? nextRun = frequency switch
            {
                FrequencyType.Once => null,
                FrequencyType.Daily => schedule.NextRunAt.Value.AddDays(1),
                FrequencyType.Weekly => CalculateNextWeeklyRun(schedule),
                FrequencyType.Monthly => CalculateMonthlyRun(schedule.NextRunAt.Value),
                FrequencyType.Yearly => CalculateYearlyRun(schedule.NextRunAt.Value),
                FrequencyType.Custom => null,
                _ => null
            };
            return ValidateEndDate(schedule, nextRun);
        }

        private static DateTime? CalculateWeeklyRun(Schedule schedule, DateTime startDateTime)
        {
            if (string.IsNullOrWhiteSpace(schedule.WeekDays))
                return startDateTime;

            var selectedDays = ParseWeekDays(schedule.WeekDays);
            for (int i = 0; i < 7; i++)
            {
                DateTime candidate = startDateTime.AddDays(i);
                if (selectedDays.Contains(candidate.DayOfWeek))
                    return candidate;
            }
            return null;
        }

        private static DateTime? CalculateNextWeeklyRun(Schedule schedule)
        {
            if (!schedule.NextRunAt.HasValue || string.IsNullOrWhiteSpace(schedule.WeekDays))
                return null;

            var selectedDays = ParseWeekDays(schedule.WeekDays);
            DateTime currentRun = schedule.NextRunAt.Value;
            for (int i = 1; i <= 7; i++)
            {
                DateTime candidate = currentRun.AddDays(i);
                if (selectedDays.Contains(candidate.DayOfWeek))
                    return candidate;
            }
            return null;
        }

        private static HashSet<DayOfWeek> ParseWeekDays(string weekDays)
        {
            return weekDays
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(day => day.Trim().ToUpperInvariant())
                .Select(day => day switch
                {
                    "SUN" => DayOfWeek.Sunday,
                    "MON" => DayOfWeek.Monday,
                    "TUE" => DayOfWeek.Tuesday,
                    "WED" => DayOfWeek.Wednesday,
                    "THU" => DayOfWeek.Thursday,
                    "FRI" => DayOfWeek.Friday,
                    "SAT" => DayOfWeek.Saturday,
                    _ => throw new ArgumentException($"Invalid weekday: {day}")
                })
                .ToHashSet();
        }

        private static DateTime CalculateMonthlyRun(DateTime currentRun)
        {
            DateTime nextMonth = currentRun.AddMonths(1);
            int lastDay = DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month);
            int day = Math.Min(currentRun.Day, lastDay);
            return new DateTime(nextMonth.Year, nextMonth.Month, day, currentRun.Hour, currentRun.Minute, currentRun.Second);
        }

        private static DateTime CalculateYearlyRun(DateTime currentRun)
        {
            int nextYear = currentRun.Year + 1;
            int lastDay = DateTime.DaysInMonth(nextYear, currentRun.Month);
            int day = Math.Min(currentRun.Day, lastDay);
            return new DateTime(nextYear, currentRun.Month, day, currentRun.Hour, currentRun.Minute, currentRun.Second);
        }

        private static DateTime? ValidateEndDate(Schedule schedule, DateTime? nextRun)
        {
            if (!nextRun.HasValue)
                return null;
            if (schedule.EndDate.HasValue && nextRun.Value.Date > schedule.EndDate.Value.Date)
                return null;
            return nextRun;
        }
    }
}