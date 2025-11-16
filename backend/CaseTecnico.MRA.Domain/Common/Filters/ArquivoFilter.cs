
namespace CaseTecnico.MRA.Domain.Common.Filters;

public class ArquivoFilter : BaseFilter
{
    public Guid? EmpresaId { get; set; }
    public Guid? ArquivoStatusId { get; set; }
}
