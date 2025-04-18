using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.DTOs;
using Schedule.Application.Handlers;
using Schedule.Infrastructure.Persistence; // <-- certifique-se de que este namespace está correto

namespace Schedule.API.Controllers
{
    [ApiController]
    [Route("api/user-account")]
    public class UserController : ControllerBase
    {
        private readonly CreateUserHandler _handler;
        private readonly AppDbContext _context; // <-- Adiciona o EFContext

        public UserController(CreateUserHandler handler, AppDbContext context) // <-- Injeta o EFContext
        {
            _handler = handler;
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _handler.Handle(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Logando detalhes da exceção
                var errorDetails = ex.InnerException != null
                    ? $"{ex.Message} | Inner Exception: {ex.InnerException.Message}"
                    : ex.Message;

                return BadRequest(new { error = errorDetails });
            }
        }

        // GET: api/user-account
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _context.UserAccounts.ToListAsync(cancellationToken);
                if (users == null || !users.Any())
                {
                    return NotFound(new { message = "No users found" });
                }

                return Ok(users); // Retorna a lista de usuários
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // GET: api/user-account/getById{id}
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.UserAccounts
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                return Ok(user); // Retorna o usuário encontrado
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                // Buscar o usuário pelo id
                var user = await _context.UserAccounts
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                // Remover o usuário
                _context.UserAccounts.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);

                return Ok(new { message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


    }
}
