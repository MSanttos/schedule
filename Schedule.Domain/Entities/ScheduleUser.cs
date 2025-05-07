namespace Schedule.Domain.Entities
{
    public class Schedulers
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan Duration { get; set; }
        public string? Notes { get; set; }  // Nullable
        public string Status { get; set; } = "confirmed";  // Valor padrão
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }  // Nullable
        public string? CreatedBy { get; set; }  // Nullable
        public string? UpdatedBy { get; set; }  // Nullable
        public string? ClientName { get; set; }  // Nullable
        public string? ServiceName { get; set; }  // Nullable

        public Schedulers(Guid clientId, string clientName, Guid serviceId, string serviceName, DateTime date, TimeSpan time, TimeSpan duration, string? notes, string status, decimal price, string createdBy)
        {
            ClientId = clientId;
            ClientName = clientName;
            ServiceId = serviceId;
            ServiceName = serviceName;
            Date = date;
            Time = time;
            Duration = duration;
            Notes = notes;
            Status = status;
            Price = price;
            CreatedBy = createdBy;
        }

        public void UpdateStatus(string status, string updatedBy)
        {
            Status = status;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
