
namespace CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;

public class CreateArquivoFromUploadDto
{
    public List<CreateArquivoFromUploadRecepcionadoDto> Recepcionados { get; set; } = [];

    public List<CreateArquivoFromUploadNaoRecepcionadoDto> NaoRecepcionados { get; set; } = [];
}

public class CreateArquivoFromUploadRecepcionadoDto
{
    public DateTime DataProcessamento { get; set; }
    public DateTime? PeriodoInicial { get; set; }
    public DateTime? PeriodoFinal { get; set; }
    public string Estabelecimento { get; set; } = null!;
    public string Sequencia { get; set; } = null!;
    public string EstruturaImportada { get; set; } = null!;
    public Guid EmpresaId { get; set; }
}

public class CreateArquivoFromUploadNaoRecepcionadoDto
{
    public string Motivos { get; set; } = null!;
    public string EstruturaImportada { get; set; } = null!;

    public void AddNewMotivo(string motivo)
    {
        var motivosList = string.IsNullOrWhiteSpace(Motivos)
                ? new List<string>()
                : Motivos.Split(';').ToList();

        motivosList.Add(motivo);
        Motivos = string.Join(";", motivosList);
    }
}