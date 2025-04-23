
namespace Schedule.Application.DTOs.Request
{
    public class CreateScheduleRequest
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string? Service { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Duration { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal Price { get; set; }
        public string CreatedBy { get; set; } = "system";
    }

    public class CreateScheduleResponse
    {
        public string Message { get; set; } = "Schedule created successfully.";
    }
}
