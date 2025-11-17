
using CaseTecnico.MRA.Domain.Common;
using CaseTecnico.MRA.Domain.Common.Extensions;
using CaseTecnico.MRA.Domain.Common.Filters;
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;
using CaseTecnico.MRA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnico.MRA.Infrastructure.Repositories;

public class ArquivoRecepcionadoRepository : BaseRepository<ArquivoRecepcionado>, IArquivoRecepcionadoRepository
{
    public ArquivoRecepcionadoRepository(AppDbContext context)
           : base(context)
    {
    }

    public async Task<PagedResult<ArquivoRecepcionado>> GetDatatableAsync(ArquivoRecepcionadoFilter filter, CancellationToken cancellationToken = default)
    {
        var query = _context.ArquivoRecepcionados
            .Include(i => i.Empresa)
            .AsQueryable();

        //TOTAL antes do skip / take
        var totalRecords = await query.CountAsync(cancellationToken);

        // ORDERNAÇÃO(dinâmica)
        if (filter.SortField?.ToLower() == "empresadescricao")
            filter.SortField = "EmpresaId";

        query = query.ApplySorting(filter.SortField, filter.SortDirection);

        //PAGINAÇÃO
        var skip = (filter.Page - 1) * filter.PageSize;
        var data = await query
            .Skip(skip)
            .Take(filter.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new PagedResult<ArquivoRecepcionado>
        {
            Data = data,
            TotalRecords = totalRecords,
            Page = filter.Page,
            PageSize = filter.PageSize
        };
    }
}