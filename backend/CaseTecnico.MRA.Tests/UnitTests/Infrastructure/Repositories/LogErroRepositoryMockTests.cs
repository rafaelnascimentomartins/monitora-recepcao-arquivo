
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Infrastructure.Context;
using CaseTecnico.MRA.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace CaseTecnico.MRA.Tests.UnitTests.Infrastructure.Repositories;

public class LogErroRepositoryMockTests
{
    [Fact]
    public async Task InsertAsync_ChamaInsertAsync()
    {
        // Arrange
        var mockSet = new Mock<DbSet<LogErro>>();

        // Configura AddAsync para simplesmente retornar ValueTask completado
        mockSet.Setup(m => m.AddAsync(It.IsAny<LogErro>(), It.IsAny<CancellationToken>()))
       .Returns((LogErro entity, CancellationToken token) => default(ValueTask<EntityEntry<LogErro>>));


        var mockContext = new Mock<IAppDbContext>();
        mockContext.Setup(c => c.LogErros).Returns(mockSet.Object);
        mockContext.Setup(c => c.Set<LogErro>()).Returns(mockSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var repository = new LogErroRepository(mockContext.Object);

        var log = new LogErro
        {
            DataInsercao = new DateTime(2025, 10, 11),
            Message = "Teste de log"
        };

        // Act
        var result = await repository.InsertAsync(log);

        // Assert
        mockSet.Verify(m => m.AddAsync(log, It.IsAny<CancellationToken>()), Times.Once);
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(log, result);
    }

    [Fact]
    public async Task InsertAsync_LancaInsertAsyncExcecao()
    {
        // Arrange
        var mockSet = new Mock<DbSet<LogErro>>();

        // Mock AddAsync sem retornar EntityEntry real
        mockSet.Setup(m => m.AddAsync(It.IsAny<LogErro>(), It.IsAny<CancellationToken>()))
               .Returns((LogErro log, CancellationToken token) => default(ValueTask<EntityEntry<LogErro>>));

        var mockContext = new Mock<IAppDbContext>();
        mockContext.Setup(c => c.LogErros).Returns(mockSet.Object);
        mockContext.Setup(c => c.Set<LogErro>()).Returns(mockSet.Object);

        // Simula exceção ao salvar
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                   .ThrowsAsync(new Exception("Erro ao salvar no banco"));

        var repository = new LogErroRepository(mockContext.Object);

        var log = new LogErro
        {
            DataInsercao = new DateTime(2025, 10, 11),
            Message = "Teste de log"
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => repository.InsertAsync(log));
        Assert.Equal("Erro ao salvar no banco", exception.Message);
    }
}
