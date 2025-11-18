
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Infrastructure.Context;
using CaseTecnico.MRA.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnico.MRA.Tests.IntegrationTests.Infrastructure.Repositories;

public class EmpresaRepositoryInMemoryTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "EmpresaDb")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetAsDictionaryAsync_RetornaDicionarioComValores()
    {
        // Arrange
        await using var context = GetInMemoryDbContext();

        context.Empresas.AddRange(new List<Empresa>
    {
        new Empresa { 
            DataInsercao = new DateTime(2025, 10, 11),
            Descricao = "Empresa A"
        },
         new Empresa {
            DataInsercao = new DateTime(2025, 10, 11),
            Descricao = "Empresa B"
        },
    });
        await context.SaveChangesAsync();

        var repository = new EmpresaRepository(context);

        // Act
        var result = await repository.GetAsDictionaryAsync(e => e.Descricao);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Empresa A", result["Empresa A"].Descricao);
        Assert.True(result.ContainsKey("Empresa B"));
        Assert.Equal("Empresa B", result["Empresa B"].Descricao);
    }
}
