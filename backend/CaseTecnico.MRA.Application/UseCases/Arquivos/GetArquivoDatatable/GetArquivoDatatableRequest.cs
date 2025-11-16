using CaseTecnico.MRA.Application.Common;

namespace CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDatatable;

public class GetArquivoDatatableRequest : BaseFilter
{
    public Guid? EmpresaId { get; set; }
    public Guid? ArquivoStatusId { get; set; }
}
