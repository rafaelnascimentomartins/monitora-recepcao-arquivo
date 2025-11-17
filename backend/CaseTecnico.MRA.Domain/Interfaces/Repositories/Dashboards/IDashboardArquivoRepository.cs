
using CaseTecnico.MRA.Domain.Common.Models.Dashboards;

namespace CaseTecnico.MRA.Domain.Interfaces.Repositories.Dashboards;

public interface IDashboardArquivoRepository
{
    Task<DashArquivoResumoStatusModel> GetResumoStatusAsync(CancellationToken cancellationToken = default);
}
