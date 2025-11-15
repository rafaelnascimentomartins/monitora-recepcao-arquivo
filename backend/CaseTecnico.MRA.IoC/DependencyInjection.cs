

using CaseTecnico.MRA.Infrastructure.Context;
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
               configuration.GetConnectionString("MRAConnection")));

        services.RegisterRepositories();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {


        return services;
    }

   
    private static void RegisterRepositories(this IServiceCollection services)
    {

        

    }
}
