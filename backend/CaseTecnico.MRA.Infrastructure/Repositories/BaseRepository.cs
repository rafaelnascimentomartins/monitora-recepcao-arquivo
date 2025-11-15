
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;
using CaseTecnico.MRA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task DeleteByIdAsync(Guid id)
    {
        var entity = await _dbSet
            .FirstOrDefaultAsync(x => x.Identificador == id);

        if (entity is null)
            return;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual Task<TEntity?> GetByIdAsync(Guid id)
    {
        return _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Identificador == id);
    }
}
