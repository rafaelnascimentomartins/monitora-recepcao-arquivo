using CaseTecnico.MRA.Api.Middlewares;
using CaseTecnico.MRA.Application.Settings;
using CaseTecnico.MRA.Infrastructure.Context;
using CaseTecnico.MRA.IoC;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Carrega appsettings padrão
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

//VARIÁVEIS
var allowedOrigins = builder.Configuration.GetSection("AllowedOriginsCors").Get<string[]>();

//MAPEIA O APPSETTINGS
var appSettings = builder.Configuration.Get<AppSettings>();
builder.Services.AddSingleton(appSettings);

//Registrar Application ( FluentValidation )
builder.Services.AddApplication();

// Registrar Infrastructure (DbContext, Identity, Repositories)
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //CONFIGURAÇÃO REALIZADA DENTRO DO csproj PARA HABILITAR O XML PARA COMENTÁRIOS NO SWAGGER.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//CONFIGURAÇÃO DE CORS PARA ACESSO VIA BROWSER
//PERMITINDO APENAS ROTAS DE ACORDO COM APPSETTINGS: AllowedOriginsCors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOriginsCors", policy =>
    {
        policy.WithOrigins(allowedOrigins!)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

//APLICAÇÃO DE Migrate PARA EXECUÇÃO AUTOMÁTICA DO SNAPSHOT MIGRATION
//MAS APENAS SE NÃO FOR AMBIENTE PRODUTIVO
if (!app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
}

//BLOQUEIO PARA USO FORA DE BROWSER, COMO POSTMAN. PARA USO É APENAS UTILIZAR O HEADER:
//X-API-KEY e senha 
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value ?? string.Empty;
    var refer = context.Request.Headers["Referer"].ToString();

    if(Debugger.IsAttached)
    {
        await next();
        return;
    }

    // VERIFICANDO SE A ORIGEM DE QUEM CHAMOU VEIO DO SWAGGER.
    if (!string.IsNullOrEmpty(refer) && refer.Contains("/swagger", StringComparison.OrdinalIgnoreCase)
    || !string.IsNullOrEmpty(path) && path.Contains("/swagger", StringComparison.OrdinalIgnoreCase))

    {
        await next();
        return;
    }
    else if (path.Contains("/ping"))
    {
        await next();
        return;
    }

    var origin = context.Request.Headers["Origin"].ToString(); //COM VALOR APENAS EM BROWSERS
    var apiKey = builder.Configuration["ApiSettings:ApiKey"];
    var apiSecret = builder.Configuration["ApiSettings:ApiSecret"];

    if (!allowedOrigins!.Contains(origin))
    {
        if (!context.Request.Headers.TryGetValue("X-API-KEY", out var requestKey) || requestKey != apiKey ||
       !context.Request.Headers.TryGetValue("X-API-SECRET", out var requestSecret) || requestSecret != apiSecret)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync(@$"
                Acesso nao autorizado! X-API-KEY e X-API-SECRET nao definidos.");
            return;
        }
    }

    await next();
});


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MRA API V1");
    c.RoutePrefix = "swagger";  //ALTERANDO PARA ROTA: swagger, POR CONTA DA VALIDAÇÃO DE ACESSO.
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
//MIDDLEWARE PARA LOGS GLOBAIS DE EXCEÇÃO
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowedOriginsCors");
app.Run();
