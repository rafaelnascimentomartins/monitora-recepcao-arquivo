
namespace CaseTecnico.MRA.Application.UseCases.Arquivos.GetDatatableArquivo;

public class GetArquivoDatatableResponse
{
    // ----- Campos da Entidade / DTO -----
    public int Identificador { get; set; }
    public int EmpresaId { get; set; }

    // ----- Paginação -----
    public int TotalRecords { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
