using CaseTecnico.MRA.Domain.Common;
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;
using CaseTecnico.MRA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CaseTecnico.MRA.Infrastructure.Repositories;

public class ArquivoRepository : BaseRepository<Arquivo>, IArquivoRepository
{
    public ArquivoRepository(AppDbContext context)
           : base(context)
    {
    }

    public async Task<PagedResult<Arquivo>> GetDatatableAsync(CancellationToken cancellationToken = default)
    {
        var query = _context.Arquivos
            .Include(i => i.ArquivoStatus)
            .Include(i => i.Empresa)
            .AsQueryable();

        //FILTROS


        //TOTAL antes do skip / take
        var totalRecords = await query.CountAsync(cancellationToken);

        //PAGINAÇÃO
        var data = await query
            .Skip(0)
            .Take(10)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new PagedResult<Arquivo>
        {
            Data = data,
            TotalRecords = totalRecords,
            Page = 1,
            PageSize = 10
        };
    }
}
