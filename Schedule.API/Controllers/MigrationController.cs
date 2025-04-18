using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schedule.Infrastructure.Persistence;

namespace Schedule.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MigrationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MigrationController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Aplica todas as migrations pendentes ao banco de dados.
        /// </summary>
        //[HttpPost("migrate")]
        //public async Task<IActionResult> MigrateAsync()
        //{
        //    try
        //    {
        //        await _context.Database.MigrateAsync();
        //        return Ok("✅ Migration aplicada com sucesso.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"❌ Erro ao aplicar migration: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Atualiza o banco de dados com base nas migrations existentes.
        /// </summary>
        [HttpPost("update")]
        public async Task<IActionResult> UpdateDatabaseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync(); // Pode ser o mesmo comando
                return Ok("✅ Banco de dados atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Erro ao atualizar banco: {ex.Message}");
            }
        }
    }
}
