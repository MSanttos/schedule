using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Schedule.Application.DTOs.Request;
using Schedule.Application.DTOs.Response;
using Schedule.Application.Handlers;
using Schedule.Infrastructure.Persistence;
using Serilog.Core;

namespace Schedule.API.Controllers
{
    [ApiController]
    [Route("api/user-account")]
    public class UserController : ControllerBase
    {
        private readonly CreateUserHandler _handler;
        private readonly AppDbContext _context; // <-- Adiciona o EFContext
        private readonly UpdateUserHandler _updateHandler;

        public UserController(CreateUserHandler handler, UpdateUserHandler updateHandler, AppDbContext context) // <-- Injeta o EFContext
        {
            _handler = handler;
            _context = context;
            _updateHandler = updateHandler;
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
                // Recupera os usuários do banco
                var users = await _context.UserAccounts
                    .Select(u => new UserResponseDto
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        BirthDate = u.BirthDate ?? DateTime.MinValue,
                        Gender = u.Gender.HasValue ? (int)u.Gender : 99,
                        MaritalStatus = u.MaritalStatus.HasValue ? (int)u.MaritalStatus : 99,
                        City = u.City,
                        State = u.State,
                        PostalCode = u.PostalCode
                    })
                    .ToListAsync(cancellationToken);

                // Se não houver usuários, retorna 404
                if (users == null || !users.Any())
                {
                    return NotFound(new { message = "No users found" });
                }

                // Retorna a lista de usuários no formato DTO
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Em caso de erro, retorna 500
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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        {
            ModelState.Remove("PasswordHash");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Atribui o ID da rota ao request
            request.Id = id;

            try
            {
                var result = await _updateHandler.Handle(request, cancellationToken);

                return Ok(new
                {
                    Message = "Usuário atualizado com sucesso",
                    User = new
                    {
                        result.Id,
                        result.Name,
                        result.Email,
                        result.PhoneNumber,
                        result.City,
                        result.State
                    }
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

    }
}
