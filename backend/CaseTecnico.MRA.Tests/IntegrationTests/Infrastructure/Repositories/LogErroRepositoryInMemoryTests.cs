
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Infrastructure.Context;
using CaseTecnico.MRA.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnico.MRA.Tests.IntegrationTests.Infrastructure.Repositories;

public class LogErroRepositoryInMemoryTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "LogErroDb")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task InsertAsync_InsereEntidadeNoBanco()
    {
        // Arrange
        await using var context = GetInMemoryDbContext();

        var repository = new LogErroRepository(context);

        var log = new LogErro
        {
           DataInsercao = new DateTime(2025, 10, 11),
           InnerException = null,
           StackTrace = null,
           Message = "Test Log Fail"
        };

        // Act
        var result = await repository.InsertAsync(log);

        // Assert
        var logsNoBanco = await context.LogErros.ToListAsync();
        Assert.Single(logsNoBanco);                   // só existe 1 registro
        Assert.Equal("Test Log Fail", logsNoBanco[0].Message);
        Assert.Equal(log, result);                    // retorno deve ser a entidade inserida
        Assert.NotEqual(Guid.Empty, logsNoBanco[0].Identificador);         // Id gerado automaticamente (Identity)
    }
}
