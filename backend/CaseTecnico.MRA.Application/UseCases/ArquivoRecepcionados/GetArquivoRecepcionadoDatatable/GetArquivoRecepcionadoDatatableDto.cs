
namespace CaseTecnico.MRA.Application.UseCases.ArquivoRecepcionados.GetArquivoRecepcionadoDatatable;

public class GetArquivoRecepcionadoDatatableDto
{
    public Guid Identificador { get; set; }
    
    public DateTime DataInsercao { get; set; }

    public string Estabelecimento { get; set; } = null!;

    public DateTime DataProcessamento { get; set; }

    public DateTime? PeriodoInicial { get; set; }

    public DateTime? PeriodoFinal { get; set; }

    public string Sequencia { get; set; } = null!;

    public string? EmpresaDescricao { get; set; }
}
