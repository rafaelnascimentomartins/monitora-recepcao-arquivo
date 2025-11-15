using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;
using CaseTecnico.MRA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnico.MRA.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (entities == null || !entities.Any())
            return Enumerable.Empty<TEntity>();

        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entities;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet
            .FirstOrDefaultAsync(x => x.Identificador == id, cancellationToken);

        if (entity is null)
            return;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual Task<TEntity?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Identificador == id, cancellationToken);
    }

    /// <summary>
    /// Retorna todos os registros como Dictionary para busca rápida em memória.
    /// </summary>
    public virtual async Task<Dictionary<TKey, TEntity>> GetAsDictionaryAsync<TKey>(
        Func<TEntity, TKey> keySelector,
        CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        var list = await _dbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return list.ToDictionary(keySelector);
    }
}

