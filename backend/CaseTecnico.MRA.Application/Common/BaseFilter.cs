
namespace CaseTecnico.MRA.Application.Common;

public abstract class BaseFilter
{
    // Paginação
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    // Ordenação
    public string? SortField { get; set; }  // Ex: "Nome", "DataCriacao"
    public string? SortDirection { get; set; } = "asc"; // asc | desc
}
