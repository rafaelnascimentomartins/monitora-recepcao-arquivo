
//using AutoMapper;
//using CaseTecnico.MRA.Application.UseCases.Arquivos.GetDatatableArquivo;
//using CaseTecnico.MRA.Domain.Common;
//using CaseTecnico.MRA.Domain.Common.Filters;
//using CaseTecnico.MRA.Domain.Interfaces.Repositories;

//namespace CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDatatable;

//public class GetArquivoDatatableHandler
//{
//    private readonly IArquivoRepository _repository;
//    private readonly IMapper _mapper;

//    public GetArquivoDatatableHandler(
//        IArquivoRepository repository,
//        IMapper mapper)
//    {
//        _repository = repository;
//        _mapper = mapper;
//    }

//    public async Task<GetArquivoDatatableResponse> Handle(
//        GetArquivoDatatableRequest request,
//         CancellationToken cancellationToken = default)
//    {
//        var filter = new ArquivoFilter
//        {
//            Page = request.Page,
//            PageSize = request.PageSize,
//            SortField = request.SortField,
//            SortDirection = request.SortDirection,
//            EmpresaId = request.EmpresaId,
//            ArquivoStatusId = request.ArquivoStatusId
//        };

//        var pagedResult = await _repository.GetDatatableAsync(filter, cancellationToken);

//        var mapperData = _mapper.Map<List<GetArquivoDatatableDto>>(pagedResult.Data);

//        return new GetArquivoDatatableResponse
//        {
//            Data = mapperData,
//            TotalRecords = pagedResult.TotalRecords,
//            Page = pagedResult.Page,
//            PageSize = pagedResult.PageSize,
//        };
//    }
//}
