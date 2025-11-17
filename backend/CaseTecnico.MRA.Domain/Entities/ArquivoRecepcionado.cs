
namespace CaseTecnico.MRA.Domain.Entities;

public class ArquivoRecepcionado : BaseEntity
{
    public string Estabelecimento { get; set; } = null!;

    public DateTime DataProcessamento { get; set; }

    public DateTime? PeriodoInicial { get; set; }

    public DateTime? PeriodoFinal { get; set; }

    public string Sequencia { get; set; } = null!;

    public string EstruturaImportada { get; set; } = null!;

    // FKs de banco
    public Guid EmpresaId { get; set; }


    //MODELOS PARA INCLUDE
    public Empresa? Empresa { get; set; }
}
