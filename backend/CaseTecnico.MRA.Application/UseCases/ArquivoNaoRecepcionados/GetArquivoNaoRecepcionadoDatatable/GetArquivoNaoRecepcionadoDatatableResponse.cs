
namespace CaseTecnico.MRA.Application.UseCases.ArquivoNaoRecepcionados.GetArquivoNaoRecepcionadoDatatable;

public class GetArquivoNaoRecepcionadoDatatableResponse
{
    public List<GetArquivoNaoRecepcionadoDatatableDto> Data { get; set; } = new();

    // ----- Paginação -----
    public int TotalRecords { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
