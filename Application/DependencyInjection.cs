using Microsoft.Extensions.DependencyInjection;
using Teste.ScoreAPI.Application.Interfaces;
using Teste.ScoreAPI.Application.Services;

namespace Teste.ScoreAPI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IScoreCalculator, ScoreCalculator>();

        return services;
    }
}
