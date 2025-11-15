
namespace CaseTecnico.MRA.Domain.Entities;

public sealed class Empresa : BaseEntity
{
    public string Descricao { get; set; } = string.Empty;

    public ICollection<Arquivo>? Arquivos { get; set; }
}
