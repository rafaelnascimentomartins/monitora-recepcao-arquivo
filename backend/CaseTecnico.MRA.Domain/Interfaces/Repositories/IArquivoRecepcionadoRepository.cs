
using CaseTecnico.MRA.Domain.Common;
using CaseTecnico.MRA.Domain.Common.Filters;
using CaseTecnico.MRA.Domain.Entities;

namespace CaseTecnico.MRA.Domain.Interfaces.Repositories;

public interface IArquivoRecepcionadoRepository : IBaseRepository<ArquivoRecepcionado>
{
    Task<PagedResult<ArquivoRecepcionado>> GetDatatableAsync(ArquivoRecepcionadoFilter filter, CancellationToken cancellationToken = default);
}
