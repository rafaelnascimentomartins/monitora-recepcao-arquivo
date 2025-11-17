
using CaseTecnico.MRA.Domain.Common;
using CaseTecnico.MRA.Domain.Common.Extensions;
using CaseTecnico.MRA.Domain.Common.Filters;
using CaseTecnico.MRA.Domain.Common.Models.Dashboards;
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Domain.Interfaces.Repositories.Dashboards;
using CaseTecnico.MRA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnico.MRA.Infrastructure.Repositories.Dashboards;

public class DashboardArquivoRepository : IDashboardArquivoRepository
{
    private readonly AppDbContext _context;

    public DashboardArquivoRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<DashArquivoResumoStatusModel> GetResumoStatusAsync(CancellationToken cancellationToken = default)
    {
        int countRecepcionados = await _context.ArquivoRecepcionados.AsNoTracking().CountAsync(cancellationToken);
        int countNaoRecepcionados = await _context.ArquivoNaoRecepcionados.AsNoTracking().CountAsync(cancellationToken);

        return new DashArquivoResumoStatusModel
        {
            QtdRecepcionados = countRecepcionados ,
            QtdNaoRecepcionados = countNaoRecepcionados
        };
    }
}
