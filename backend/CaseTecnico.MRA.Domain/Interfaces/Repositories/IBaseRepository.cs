
using CaseTecnico.MRA.Domain.Entities;
using System.Linq.Expressions;

namespace CaseTecnico.MRA.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteByIdAsync(Guid id);
    Task<TEntity?> GetByIdAsync(Guid id);
}
