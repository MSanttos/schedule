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

//ScheduleRepository
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();  // Registro do repositório de agendamentos

builder.Services.AddScoped<IPasswordHasher<UserAccount>, PasswordHasher<UserAccount>>();
builder.Services.AddScoped<CreateUserHandler>();  // Registro do handler para criar usuário
builder.Services.AddScoped<UpdateUserHandler>();  // Update
builder.Services.AddScoped<CreateScheduleHandler>();  // Registro do handler para agendamentos


// Configuração de controllers, documentação e outros serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// 👇 ATIVAR CORS AQUI
app.UseCors();

app.UseAuthorization();
app.MapControllers();
app.Run();
