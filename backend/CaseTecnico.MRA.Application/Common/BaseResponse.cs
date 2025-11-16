
namespace CaseTecnico.MRA.Application.Common;

public abstract class BaseResponse
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida
    /// </summary>
    public bool Sucesso { get; set; } = true;

    /// <summary>
    /// Mensagens de erro ou informação
    /// </summary>
    public List<string> Mensagens { get; set; } = new();
}
