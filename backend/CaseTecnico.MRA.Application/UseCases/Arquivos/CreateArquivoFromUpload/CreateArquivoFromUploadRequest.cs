
namespace CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;

public class CreateArquivoFromUploadRequest
{
    public Stream ArquivoStream { get; set; } = null!;
    public string FileName { get; set; } = null!;
}
