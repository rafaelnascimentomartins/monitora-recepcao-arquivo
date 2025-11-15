
namespace CaseTecnico.MRA.Domain.Entities;

public sealed class Arquivo : BaseEntity
{
    public int TipoRegistro { get; set; }

    public int Estabelecimento { get; set; }

    public DateTime DataProcessamento { get; set; }

    public DateTime? PeriodoInicial { get; set; }

    public DateTime? PeriodoFinal { get; set; }

    public int Sequencia { get; set; }

    public string Empresa { get; set; } = string.Empty;
}
