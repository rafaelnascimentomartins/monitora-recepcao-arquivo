
using CaseTecnico.MRA.Application.Common;

namespace CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;

public class CreateArquivoFromUploadResponse : BaseResponse
{
    public List<Guid> IdentificadoresCriados { get; set; } = new();
}
