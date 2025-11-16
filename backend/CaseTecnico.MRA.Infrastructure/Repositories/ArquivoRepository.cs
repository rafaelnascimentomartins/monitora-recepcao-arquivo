using CaseTecnico.MRA.Domain.Common;
using CaseTecnico.MRA.Domain.Common.Extensions;
using CaseTecnico.MRA.Domain.Common.Filters;
using CaseTecnico.MRA.Domain.Common.Models;
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;
using CaseTecnico.MRA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CaseTecnico.MRA.Infrastructure.Repositories;

public class ArquivoRepository : BaseRepository<Arquivo>, IArquivoRepository
{
    public ArquivoRepository(AppDbContext context)
           : base(context)
    {
    }

    public async Task<PagedResult<Arquivo>> GetDatatableAsync(ArquivoFilter filter, CancellationToken cancellationToken = default)
    {
        var query = _context.Arquivos
            .Include(i => i.ArquivoStatus)
            .Include(i => i.Empresa)
            .AsQueryable();

        //FILTROS
        if (filter.EmpresaId.HasValue)
            query = query.Where(w => w.EmpresaId == filter.EmpresaId.Value);

        if (filter.ArquivoStatusId.HasValue)
            query = query.Where(w => w.ArquivoStatusId == filter.ArquivoStatusId.Value);
        
        //TOTAL antes do skip / take
        var totalRecords = await query.CountAsync(cancellationToken);

        // ORDERNAÇÃO (dinâmica)
        query = query.ApplySorting(filter.SortField, filter.SortDirection);

        //PAGINAÇÃO
        var skip = (filter.Page - 1) * filter.PageSize;
        var data = await query
            .Skip(skip)
            .Take(filter.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new PagedResult<Arquivo>
        {
            Data = data,
            TotalRecords = totalRecords,
            Page = filter.Page,
            PageSize = filter.PageSize
        };
    }

    public async Task<List<ArquivoResumoStatusModel>> GetResumoStatusAsync(CancellationToken cancellationToken = default)
    {
        var query = _context.Arquivos.AsNoTracking()
        .Include(a => a.ArquivoStatus)
        .AsQueryable();

        var resumo = await query
            .GroupBy(a => new { a.ArquivoStatusId, a.ArquivoStatus!.Descricao })
            .Select(g => new ArquivoResumoStatusModel
            {
                ArquivoStatusDescricao = g.Key.Descricao,
                Count = g.Count()
            })
            .ToListAsync(cancellationToken);

        return resumo;
    }
}
