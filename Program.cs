using Teste.ScoreAPI.Api.Middleware;
using Teste.ScoreAPI.Application.Interfaces;
using Teste.ScoreAPI.Application.Services;
using Teste.ScoreAPI.Domain.Interfaces;
using Teste.ScoreAPI.Infrastructure.Repositories;
using Teste.ScoreAPI.Infrastructure.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
using Teste.ScoreAPI.Infrastructure.Database;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não configurada.");

builder.Services.AddSingleton<IDbConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
builder.Services.AddScoped<ICpfValidator, CpfValidator>();
builder.Services.AddScoped<IScoreCalculator, ScoreCalculator>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
