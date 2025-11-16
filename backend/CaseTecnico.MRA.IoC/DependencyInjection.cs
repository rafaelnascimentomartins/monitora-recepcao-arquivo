using CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivo;
using CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;
using CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDashResumoStatus;
using CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDatatable;
using CaseTecnico.MRA.CrossCutting.Interfaces.Services;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;
using CaseTecnico.MRA.Infrastructure.Context;
using CaseTecnico.MRA.Infrastructure.Repositories;
using CaseTecnico.MRA.Infrastructure.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        services.RegisterServices();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.RegisterHandles();
        services.RegisterMappers();
        services.RegisterValidators();
        return services;
    }

   
    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IArquivoRepository, ArquivoRepository>();
        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<ILogErroRepository, LogErroRepository>();
    }
    private static void RegisterHandles(this IServiceCollection services)
    {
        services.AddScoped<CreateArquivoFromUploadHandler>();
        services.AddScoped<GetArquivoDatatableHandler>();
        services.AddScoped<GetArquivoDashResumoStatusHandler>();
    }
    private static void RegisterMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
    private static void RegisterValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateArquivoFromUploadDto>, CreateArquivoFromUploadValidator>();
    }
    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IFileEncryptionService, FileEncryptionService>();
    }
}
