using EmailSchedulerApp.ViewModels.Dashboard;

public class DashboardDto
{
    public int TotalEmails { get; set; }
    public int Pending { get; set; }
    public int Sent { get; set; }
    public int Failed { get; set; }

    public required List<RecentEmailViewModel> RecentEmails { get; set; }
}