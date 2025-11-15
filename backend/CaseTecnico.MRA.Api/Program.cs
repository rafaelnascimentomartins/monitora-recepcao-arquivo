using CaseTecnico.MRA.Infrastructure.Context;
using CaseTecnico.MRA.IoC;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Carrega appsettings padrão
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

//Registrar Application ( FluentValidation )
builder.Services.AddApplication();

// Registrar Infrastructure (DbContext, Identity, Repositories)
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // URL do Angular
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

app.UseSwagger();
app.UseSwaggerUI();
// app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAngularApp");
app.Run();
