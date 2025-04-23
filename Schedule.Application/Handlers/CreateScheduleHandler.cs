using Schedule.Application.DTOs.Request;
using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces;

namespace Schedule.Application.Handlers
{
    public class CreateScheduleHandler
    {
        private readonly IScheduleRepository _repository;

        public CreateScheduleHandler(IScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateScheduleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Converte 'Time' para TimeSpan. Aqui, 'Time' deve ser no formato "HH:mm".
                var time = TimeSpan.Parse(request.Time); // "16:30" => TimeSpan de 16 horas e 30 minutos

                // Converte 'Duration' para TimeSpan, considerando que é em minutos
                var duration = TimeSpan.FromMinutes(Convert.ToDouble(request.Duration)); // "30" => TimeSpan de 30 minutos

                var Schedule = new Schedulers(
                    request.ClientId,
                    request.ClientName,
                    request.ServiceId,
                    request.ServiceName,
                    request.Date,
                    time,
                    duration,
                    request.Notes,
                    request.Status,
                    request.Price,
                    request.CreatedBy
                );

                // Adiciona no repositório
                await _repository.AddAsync(Schedule, cancellationToken);
                return Schedule.Id;
            }
            catch (Exception ex)
            {
                // Loga o erro caso algo falhe
                var errorDetails = ex.InnerException != null
                    ? $"{ex.Message} | Inner Exception: {ex.InnerException.Message}"
                    : ex.Message;

                // Lança a exceção com detalhes
                throw new Exception($"Error processing Schedule creation: {errorDetails}");
            }
        }
    }
}
