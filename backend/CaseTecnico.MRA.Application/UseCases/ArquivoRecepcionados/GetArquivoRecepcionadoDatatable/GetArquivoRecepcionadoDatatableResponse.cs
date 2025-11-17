
using CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDatatable;

namespace CaseTecnico.MRA.Application.UseCases.ArquivoRecepcionados.GetArquivoRecepcionadoDatatable;

public class GetArquivoRecepcionadoDatatableResponse
{
    public List<GetArquivoRecepcionadoDatatableDto> Data { get; set; } = new();

    // ----- Paginação -----
    public int TotalRecords { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
