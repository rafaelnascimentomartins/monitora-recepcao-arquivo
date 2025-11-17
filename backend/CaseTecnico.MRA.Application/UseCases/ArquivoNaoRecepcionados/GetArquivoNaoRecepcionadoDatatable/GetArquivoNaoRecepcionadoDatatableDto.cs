
namespace CaseTecnico.MRA.Application.UseCases.ArquivoNaoRecepcionados.GetArquivoNaoRecepcionadoDatatable;

public class GetArquivoNaoRecepcionadoDatatableDto
{
    public Guid Identificador { get; set; }

    public DateTime DataInsercao { get; set; }

    public string EstruturaImportada { get; set; } = null!;

    public string Motivos { get; set; } = null!;
}
