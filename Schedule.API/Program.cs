using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Handlers;
using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces;
using Schedule.Infrastructure.Persistence;
using Schedule.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração de dependências
builder.Services.AddScoped<IUserRepository, UserRepository>();  // Registro do repositório de usuários
builder.Services.AddScoped<IPasswordHasher<UserAccount>, PasswordHasher<UserAccount>>();
builder.Services.AddScoped<CreateUserHandler>();  // Registro do handler para criar usuário
builder.Services.AddScoped<UpdateUserHandler>();  // Update

// Configuração de controllers, documentação e outros serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
