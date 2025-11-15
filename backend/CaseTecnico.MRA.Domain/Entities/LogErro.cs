
namespace CaseTecnico.MRA.Domain.Entities;

public sealed class LogErro : BaseEntity
{
    public string Message { get; set; } = string.Empty;
    public string? StackTrace { get; set; }
    public string? InnerException { get; set; }
}
