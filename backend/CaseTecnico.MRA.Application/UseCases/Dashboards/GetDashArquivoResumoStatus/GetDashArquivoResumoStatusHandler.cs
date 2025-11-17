using AutoMapper;
using CaseTecnico.MRA.Domain.Interfaces.Repositories.Dashboards;

namespace CaseTecnico.MRA.Application.UseCases.Dashboards.GetDashArquivoResumoStatus;

public class GetDashArquivoResumoStatusHandler
{
    private readonly IDashboardArquivoRepository _repository;
    private readonly IMapper _mapper;

    public GetDashArquivoResumoStatusHandler(
      IDashboardArquivoRepository repository,
      IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetDashArquivoResumoStatusResponse> Handle(CancellationToken cancellationToken = default)
    {
        var resultBase = await _repository.GetResumoStatusAsync(cancellationToken);

        return new GetDashArquivoResumoStatusResponse()
        {
            QtdRecepcionados = resultBase.QtdNaoRecepcionados,
            QtdNaoRecepcionados = resultBase.QtdNaoRecepcionados
        };
    }
}