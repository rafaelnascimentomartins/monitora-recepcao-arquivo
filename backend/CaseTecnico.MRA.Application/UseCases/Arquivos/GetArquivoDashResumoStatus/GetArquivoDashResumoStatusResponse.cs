

namespace CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDashResumoStatus;

public class GetArquivoDashResumoStatusResponse
{
    public List<GetArquivoDashResumoStatusDto> Data { get; set; } = new();
    
    public int Total => Data.Sum(x => x.Count);

    public DateTime DataGeracao { get; set; } = DateTime.UtcNow;
}
