namespace EmailSchedulerApp.ViewModels.Dashboard
{
    public class DashboardViewModel
{
    // Cards
    public int Total { get; set; }
    public int Pending { get; set; }
    public int Sent { get; set; }
    public int Failed { get; set; }
    // Table
    public List<RecentEmailViewModel> RecentEmails { get; set; } = new();
}
}