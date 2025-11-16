
using CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDatatable;

namespace CaseTecnico.MRA.Application.UseCases.Arquivos.GetDatatableArquivo;

public class GetArquivoDatatableResponse
{
    public List<GetArquivoDatatableDto> Data { get; set; } = new();

    // ----- Paginação -----
    public int TotalRecords { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
