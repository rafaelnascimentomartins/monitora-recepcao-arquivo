
namespace CaseTecnico.MRA.Domain.Common;

public abstract class BaseFilter
{
    // Paginação
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    // Ordenação
    public string? SortField { get; set; }  // Ex: "Nome", "DataCriacao"
    public string? SortDirection { get; set; } = "asc"; // asc | desc

    // Caso o front mande número da coluna (ex: Datatable)
    public int? SortColumnIndex { get; set; }
}
