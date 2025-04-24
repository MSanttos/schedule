namespace Schedule.Application.DTOs.Response
{
    internal class CreateScheduleResponse
    {
        public Guid ClientId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan Duration { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal Price { get; set; }
        public string CreatedBy { get; set; } = "system";
    }
}
