public class DashboardDto
{
    public int TotalEmails { get; set; }
    public int Pending { get; set; }
    public int Sent { get; set; }
    public int Failed { get; set; }

    public List<EmailSchedule> RecentEmails { get; set; }
}