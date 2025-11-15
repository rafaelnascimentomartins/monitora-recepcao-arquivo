using CaseTecnico.MRA.Application.Common;

namespace CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDatatable;

public class GetArquivoDatatableRequest : BaseFilter
{
    public int EmpresaId { get; set; }
    public int ArquivoStatusId { get; set; }
}
