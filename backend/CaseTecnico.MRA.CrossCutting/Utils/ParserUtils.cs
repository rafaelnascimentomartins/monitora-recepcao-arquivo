
namespace CaseTecnico.MRA.CrossCutting.Utils;

public static class ParserUtils
{
    /// <summary>
    /// Adiciona um novo motivo a uma string de motivos, separando com ponto e vírgula.
    /// </summary>
    public static string AdicionarMotivo(this string motivos, string novoMotivo)
    {
        if (string.IsNullOrWhiteSpace(novoMotivo))
            return motivos;

        if (string.IsNullOrWhiteSpace(motivos))
            return novoMotivo;

        return motivos + ";" + novoMotivo;
    }
}
