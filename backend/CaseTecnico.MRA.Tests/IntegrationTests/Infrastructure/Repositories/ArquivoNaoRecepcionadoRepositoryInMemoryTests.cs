
using CaseTecnico.MRA.Domain.Common.Filters;
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Infrastructure.Context;
using CaseTecnico.MRA.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnico.MRA.Tests.IntegrationTests.Infrastructure.Repositories;

public class ArquivoNaoRecepcionadoRepositoryInMemoryTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ArquivoNaoRecepcionadoDb")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetDatatableAsync_RetornaResultadosComPaginacao()
    {
        // Arrange
        await using var context = GetInMemoryDbContext();

        // Adiciona dados fictícios
        context.ArquivoNaoRecepcionados.AddRange(new List<ArquivoNaoRecepcionado>
        {
            new ArquivoNaoRecepcionado
            {
                DataInsercao = new DateTime(2025, 11, 10),
                Motivos =   "A empresa informada não existe em nossa base.",
                EstruturaImportada = "12019052632165487TestEmpresaN0002451"
            },
            new ArquivoNaoRecepcionado
            {
                DataInsercao = new DateTime(2025, 11, 11),
                Motivos = "Data processamento inválida.",
                EstruturaImportada = "09875643219999999920190625201906250000001UfCard"
            }
        });
        await context.SaveChangesAsync();

        var repository = new ArquivoNaoRecepcionadoRepository(context);

        var filter = new ArquivoNaoRecepcionadoFilter
        {
            Page = 1,
            PageSize = 2,
            SortField = "Motivos",
            SortDirection = "ASC"
        };

        // Act
        var result = await repository.GetDatatableAsync(filter, CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Data.Count());          // quantidade paginada
        Assert.Equal(2, result.TotalRecords);        // total antes do skip/take
        Assert.Equal(1, result.Page);
        Assert.Equal(2, result.PageSize);
        Assert.Equal("12019052632165487TestEmpresaN0002451", result.Data.First().EstruturaImportada);
    }

    [Fact]
    public async Task GetDatatableAsync_RetornaSemResultadosComPaginacao()
    {
        // Arrange
        await using var context = GetInMemoryDbContext(); // banco em memória limpo

        var repository = new ArquivoNaoRecepcionadoRepository(context);

        var filter = new ArquivoNaoRecepcionadoFilter
        {
            Page = 1,
            PageSize = 2,
            SortField = "Motivos",
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
