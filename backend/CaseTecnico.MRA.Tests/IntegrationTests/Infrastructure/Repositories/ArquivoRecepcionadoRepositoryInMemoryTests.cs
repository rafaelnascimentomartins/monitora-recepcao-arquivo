
using CaseTecnico.MRA.Domain.Common.Filters;
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Infrastructure.Context;
using CaseTecnico.MRA.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnico.MRA.Tests.IntegrationTests.Infrastructure.Repositories;

public class ArquivoRecepcionadoRepositoryInMemoryTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ArquivoRecepcionadoDb")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetDatatableAsync_RetornaSemResultadosComPaginacao()
    {
        // Arrange
        await using var context = GetInMemoryDbContext(); // banco em memória limpo

        var repository = new ArquivoRecepcionadoRepository(context);

        var filter = new ArquivoRecepcionadoFilter
        {
            Page = 1,
            PageSize = 2,
            SortField = "Estabelecimento",
            SortDirection = "ASC"
        };

        // Act
        var result = await repository.GetDatatableAsync(filter, CancellationToken.None);

        // Assert
        Assert.Empty(result.Data);          // Data deve estar vazia
        Assert.Equal(0, result.TotalRecords); // TotalRecords = 0
        Assert.Equal(1, result.Page);
        Assert.Equal(2, result.PageSize);
    }
}
