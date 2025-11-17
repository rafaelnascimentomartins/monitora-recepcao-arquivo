
namespace CaseTecnico.MRA.Domain.Entities;

public class ArquivoNaoRecepcionado : BaseEntity
{
    public string EstruturaImportada { get; set; } = null!;

    public string Motivos { get; set; } = null!;
}
