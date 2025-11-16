

using System.Linq.Expressions;

namespace CaseTecnico.MRA.Domain.Common.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string? sortField, string? sortDirection)
    {
        if (string.IsNullOrWhiteSpace(sortField))
            return query; // Sem ordenação, segue padrão do Repository

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(parameter, sortField);

        var lambda = Expression.Lambda(property, parameter);

        string method = sortDirection?.ToLower() == "desc"
            ? "OrderByDescending"
            : "OrderBy";

        var expression = Expression.Call(
            typeof(Queryable),
            method,
            new Type[] { typeof(T), property.Type },
            query.Expression,
            Expression.Quote(lambda)
        );

        return query.Provider.CreateQuery<T>(expression);
    }
}
