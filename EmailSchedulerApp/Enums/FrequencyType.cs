namespace EmailSchedulerApp.Enums
{
    /// <summary>
    /// Frequency types for scheduling
    /// </summary>
    public enum FrequencyType : byte
    {
        Once = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Yearly = 4,
        Custom = 5  // Cron expression
    }
}