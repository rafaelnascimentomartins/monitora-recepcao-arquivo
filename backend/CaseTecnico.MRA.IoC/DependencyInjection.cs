

using CaseTecnico.MRA.Domain.Interfaces.Repositories;
using CaseTecnico.MRA.Infrastructure.Context;
using CaseTecnico.MRA.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;

namespace CaseTecnico.MRA.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(
               configuration.GetConnectionString("MRAConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null
                )
           )
        );

        services.RegisterRepositories();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }

   
    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ILogErroRepository, LogErroRepository>();
    }
}
