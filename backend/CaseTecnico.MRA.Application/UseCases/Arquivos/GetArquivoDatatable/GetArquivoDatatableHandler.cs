
using AutoMapper;
using CaseTecnico.MRA.Application.UseCases.Arquivos.GetDatatableArquivo;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;

namespace CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDatatable;

public class GetArquivoDatatableHandler
{
    private readonly IArquivoRepository _repository;
    private readonly IMapper _mapper;

    public GetArquivoDatatableHandler(
        IArquivoRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetArquivoDatatableResponse> Handle(
         CancellationToken cancellationToken = default)
    {
        var pagedResult = await _repository.GetDatatableAsync(cancellationToken);

        var mapperResponse = _mapper.Map<GetArquivoDatatableResponse>(pagedResult.Data);

        mapperResponse.Page = pagedResult.Page;
        mapperResponse.PageSize = pagedResult.PageSize;
        mapperResponse.TotalRecords = pagedResult.TotalRecords;
        
        return mapperResponse;
    }
}
