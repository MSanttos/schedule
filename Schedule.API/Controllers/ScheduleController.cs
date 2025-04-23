using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.DTOs.Request;
using Schedule.Application.Handlers;
using Schedule.Infrastructure.Persistence;

namespace Schedule.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly CreateScheduleHandler _handler;
        private readonly AppDbContext _context;

        public ScheduleController(CreateScheduleHandler handler, AppDbContext context)
        {
            _handler = handler;
            _context = context;
        }

        [HttpPost("createSchedule")]
        public async Task<IActionResult> Create([FromBody] CreateScheduleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _handler.Handle(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorDetails = ex.InnerException != null
                    ? $"{ex.Message} | Inner Exception: {ex.InnerException.Message}"
                    : ex.Message;

                return BadRequest(new { error = errorDetails });
            }
        }

        [HttpGet("getAllSchedules")]
        public async Task<IActionResult> GetAllSchedules(CancellationToken cancellationToken)
        {
            try
            {
                var schedules = await _context.Schedulers
                    .Select(s => new
                    {
                        s.Id,
                        s.ClientId,
                        ClientName = s.ClientName ?? "Cliente não informado",
                        s.ServiceId,
                        ServiceName = s.ServiceName ?? "Serviço não informado",
                        Date = s.Date.ToString("yyyy-MM-dd"),
                        Time = s.Time.ToString(@"hh\:mm"),
                        Duration = s.Duration.ToString(@"hh\:mm"),
                        Notes = s.Notes ?? string.Empty,
                        Status = s.Status ?? "pending",
                        Price = s.Price,
                        CreatedAt = s.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                        UpdatedAt = s.UpdatedAt.HasValue ?
                            s.UpdatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : null,
                        CreatedBy = s.CreatedBy ?? "Sistema",
                        UpdatedBy = s.UpdatedBy
                    })
                    .ToListAsync(cancellationToken);

                return schedules.Any() ? Ok(schedules) : NotFound("Nenhum agendamento encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Erro ao buscar agendamentos",
                    details = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }
    }
}
