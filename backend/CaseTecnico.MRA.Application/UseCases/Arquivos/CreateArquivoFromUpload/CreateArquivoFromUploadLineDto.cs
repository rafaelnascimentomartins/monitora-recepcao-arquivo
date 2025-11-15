
namespace CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;

public class CreateArquivoFromUploadLineDto
{
    public int SequenciaLinha { get; set; }
    public DateTime DataProcessamento { get; set; }
    public DateTime? PeriodoInicial { get; set; }
    public DateTime? PeriodoFinal { get; set; }
    public long Estabelecimento { get; set; }
    public string Empresa { get; set; } = null!;
    public long Sequencia { get; set; }
    public string EstruturaImportada { get; set; } = null!;
}
