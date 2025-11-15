
using CaseTecnico.MRA.Domain.Enums;

namespace CaseTecnico.MRA.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Identificador { get; set; }
    public DateTime DataInsercao { get; set; }
}