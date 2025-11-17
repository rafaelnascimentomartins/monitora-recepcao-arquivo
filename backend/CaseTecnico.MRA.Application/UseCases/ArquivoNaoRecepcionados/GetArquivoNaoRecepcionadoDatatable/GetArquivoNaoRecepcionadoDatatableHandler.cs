
using AutoMapper;
using CaseTecnico.MRA.Application.UseCases.ArquivoRecepcionados.GetArquivoRecepcionadoDatatable;
using CaseTecnico.MRA.Domain.Common.Filters;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;

namespace CaseTecnico.MRA.Application.UseCases.ArquivoNaoRecepcionados.GetArquivoNaoRecepcionadoDatatable;

public class GetArquivoNaoRecepcionadoDatatableHandler
{
    private readonly IArquivoNaoRecepcionadoRepository _repository;
    private readonly IMapper _mapper;

    public GetArquivoNaoRecepcionadoDatatableHandler(
        IArquivoNaoRecepcionadoRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetArquivoNaoRecepcionadoDatatableResponse> Handle(
         GetArquivoNaoRecepcionadoDatatableRequest request,
         CancellationToken cancellationToken = default)
    {
        var filter = new ArquivoNaoRecepcionadoFilter
        {
            Page = request.Page,
            PageSize = request.PageSize,
            SortField = request.SortField,
            SortDirection = request.SortDirection
        };

        var pagedResult = await _repository.GetDatatableAsync(filter, cancellationToken);

        var mapperData = _mapper.Map<List<GetArquivoNaoRecepcionadoDatatableDto>>(pagedResult.Data);

        return new GetArquivoNaoRecepcionadoDatatableResponse
        {
            Data = mapperData,
            TotalRecords = pagedResult.TotalRecords,
            Page = pagedResult.Page,
            PageSize = pagedResult.PageSize,
        };
    }
}
