
using CaseTecnico.MRA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnico.MRA.Infrastructure.Context;

/// <summary>
/// Como a classe AppContext é sealed (não pode ser herdada ) e não possui DbSets virtuais
/// o Mock Test não consegue executar, pois ele exige que sejam virtuais ou que possuem uma interface
/// para referenciá-los. ( Então o IAppDbContext foi criado para o Mock Test funcionar )
/// Lemnrando que se limita a não uso de AsNoTracking,Include.... então o Mock ficou apenas para ações como:
/// Insert, Delete, Update.
/// </summary>
public interface IAppDbContext
{
    DbSet<ArquivoRecepcionado> ArquivoRecepcionados { get; }
    DbSet<ArquivoNaoRecepcionado> ArquivoNaoRecepcionados { get; }
    DbSet<Empresa> Empresas { get; }
    DbSet<LogErro> LogErros { get; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class; // IBaseRepository
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
