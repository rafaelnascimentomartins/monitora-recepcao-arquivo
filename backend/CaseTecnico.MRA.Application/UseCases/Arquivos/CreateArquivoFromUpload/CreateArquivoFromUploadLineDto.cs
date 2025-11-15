
namespace CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;

public class CreateArquivoFromUploadLineDto
{
    public int SequenciaLinha { get; set; }
    public DateTime DataProcessamento { get; set; }
    public DateTime? PeriodoInicial { get; set; }
    public DateTime? PeriodoFinal { get; set; }
    public string Estabelecimento { get; set; } = null!;
    public string Empresa { get; set; } = null!;
    public string Sequencia { get; set; } = null!;
    public string EstruturaImportada { get; set; } = null!;
}
