using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Schedule.Domain.Entities;

public class AppDbContext : DbContext
{
    private readonly ILogger<AppDbContext>? _logger;

    public DbSet<UserAccount> UserAccounts { get; set; }

    // ✔️ Construtor com logger - usado em tempo de execução
    public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger)
        : base(options)
    {
        _logger = logger;

        try
        {
            if (this.Database.CanConnect())
            {
                var connectionString = this.Database.GetDbConnection().ConnectionString;
                _logger.LogInformation("✅ Conexão com o banco bem-sucedida.");
                _logger.LogInformation("🔗 String de conexão: {ConnectionString}", connectionString);
            }
            else
            {
                _logger.LogWarning("❌ Falha na conexão com o banco.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "🚨 Erro ao tentar conectar ao banco de dados.");
        }
    }

    // ✔️ Construtor sem logger - usado em tempo de design (ef migrations)
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserAccount>().ToTable("UserAccounts");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
