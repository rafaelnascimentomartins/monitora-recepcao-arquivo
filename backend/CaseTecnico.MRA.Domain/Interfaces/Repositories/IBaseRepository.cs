
using CaseTecnico.MRA.Domain.Entities;
using System.Linq.Expressions;

namespace CaseTecnico.MRA.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
}
