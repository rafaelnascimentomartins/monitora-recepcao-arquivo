
using CaseTecnico.MRA.Domain.Common;
using CaseTecnico.MRA.Domain.Entities;

namespace CaseTecnico.MRA.Domain.Interfaces.Repositories;

public interface IArquivoRepository : IBaseRepository<Arquivo>
{
    Task<PagedResult<Arquivo>> GetDatatableAsync(CancellationToken cancellationToken);
}
