
using AutoMapper;
using CaseTecnico.MRA.Domain.Common.Filters;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;

namespace CaseTecnico.MRA.Application.UseCases.ArquivoRecepcionados.GetArquivoRecepcionadoDatatable;

public class GetArquivoRecepcionadoDatatableHandler
{
    private readonly IArquivoRecepcionadoRepository _repository;
    private readonly IMapper _mapper;

    public GetArquivoRecepcionadoDatatableHandler(
        IArquivoRecepcionadoRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetArquivoRecepcionadoDatatableResponse> Handle(
         GetArquivoRecepcionadoDatatableRequest request,
         CancellationToken cancellationToken = default)
    {
        var filter = new ArquivoRecepcionadoFilter
        {
            Page = request.Page,
            PageSize = request.PageSize,
            SortField = request.SortField,
            SortDirection = request.SortDirection
        };

        var pagedResult = await _repository.GetDatatableAsync(filter, cancellationToken);

        var mapperData = _mapper.Map<List<GetArquivoRecepcionadoDatatableDto>>(pagedResult.Data);

        return new GetArquivoRecepcionadoDatatableResponse
        {
            Data = mapperData,
            TotalRecords = pagedResult.TotalRecords,
            Page = pagedResult.Page,
            PageSize = pagedResult.PageSize,
        };
    }
}
