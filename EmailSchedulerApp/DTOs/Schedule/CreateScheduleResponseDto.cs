namespace EmailSchedulerApp.DTOs.Schedule
{
    public class CreateScheduleResponseDto
    {
        public int ScheduleId {get; set;}
        public string Message {get; set;} = string.Empty;
        public bool Success {get; set;}
        public string? FieldName { get; set; }

        public static implicit operator CreateScheduleResponseDto(CreateScheduleRequestDto v)
        {
            throw new NotImplementedException();
        }
    }
}