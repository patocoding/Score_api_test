using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Teste.ScoreAPI.Domain.Interfaces;
using Teste.ScoreAPI.Infrastructure.Database;
using Teste.ScoreAPI.Infrastructure.Repositories;
using Teste.ScoreAPI.Infrastructure.Validation;

namespace Teste.ScoreAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não configurada.");

        services.AddSingleton<IDbConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        services.AddScoped<ICustomerRepository, SqlCustomerRepository>();
        services.AddScoped<ICpfValidator, CpfValidator>();

        return services;
    }
}
