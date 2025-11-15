
namespace CaseTecnico.MRA.Domain.Entities;

public sealed class Arquivo : BaseEntity
{
    public long Estabelecimento { get; set; }

    public DateTime DataProcessamento { get; set; }

    public DateTime? PeriodoInicial { get; set; }

    public DateTime? PeriodoFinal { get; set; }

    public long Sequencia { get; set; }

    public string EstruturaImportada { get; set; } = null!;

    // FKs de banco
    public Guid ArquivoStatusId { get; set; }
    public Guid EmpresaId { get; set; }


    //MODELOS PARA INCLUDE
    public Empresa? Empresa { get; set; }
    public ArquivoStatus? ArquivoStatus { get; set; }
}
