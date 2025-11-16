
using AutoMapper;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;

namespace CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDashResumoStatus;

public class GetArquivoDashResumoStatusHandler
{
    private readonly IArquivoRepository _repository;
    private readonly IMapper _mapper;

    public GetArquivoDashResumoStatusHandler(
        IArquivoRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetArquivoDashResumoStatusResponse> Handle(CancellationToken cancellationToken = default)
    {
        var resultBase = await _repository.GetResumoStatusAsync(cancellationToken);

        var mapperData = _mapper.Map<List<GetArquivoDashResumoStatusDto>>(resultBase);

        return new GetArquivoDashResumoStatusResponse()
        {
            Data = mapperData
        };
    }
}
