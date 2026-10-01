using EmailSchedulerApp.Models;

public interface IScheduleDateService
{
    DateTime? CalculateFirstRun(Schedule schedule);
    DateTime? CalculateNextRun(Schedule schedule);
}