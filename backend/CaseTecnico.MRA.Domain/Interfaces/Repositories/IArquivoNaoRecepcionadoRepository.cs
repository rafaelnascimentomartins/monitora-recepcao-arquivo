
using CaseTecnico.MRA.Domain.Common;
using CaseTecnico.MRA.Domain.Common.Filters;
using CaseTecnico.MRA.Domain.Entities;

namespace CaseTecnico.MRA.Domain.Interfaces.Repositories;

public interface IArquivoNaoRecepcionadoRepository : IBaseRepository<ArquivoNaoRecepcionado>
{
    Task<PagedResult<ArquivoNaoRecepcionado>> GetDatatableAsync(ArquivoNaoRecepcionadoFilter filter, CancellationToken cancellationToken = default);
}
