
using CaseTecnico.MRA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnico.MRA.Infrastructure.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }


    public DbSet<Arquivo> Arquivos => Set<Arquivo>();
    public DbSet<ArquivoStatus> ArquivoStatus => Set<ArquivoStatus>();
    public DbSet<Empresa> Empresas => Set<Empresa>();
    public DbSet<LogErro> LogErros => Set<LogErro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica automaticamente todas as configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        // Desabilita cascade delete para TODAS as FKs do projeto
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
