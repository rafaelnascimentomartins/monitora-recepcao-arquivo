
namespace CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;

public class CreateArquivoFromUploadParserDto
{
    public CreateArquivoFromUploadRecepcionadoDto? Recepcionado { get; set; }
    public CreateArquivoFromUploadNaoRecepcionadoDto? NaoRecepcionado { get; set; }

    public bool Valido => Recepcionado != null; // opcional, para saber se está totalmente válido
}
