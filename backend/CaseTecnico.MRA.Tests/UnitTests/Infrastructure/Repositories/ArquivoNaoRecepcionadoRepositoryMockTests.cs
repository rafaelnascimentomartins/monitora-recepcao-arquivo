
using CaseTecnico.MRA.Domain.Common.Filters;
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Infrastructure.Context;
using CaseTecnico.MRA.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CaseTecnico.MRA.Tests.UnitTests.Infrastructure.Repositories;

public class ArquivoNaoRecepcionadoRepositoryMockTests
{
    [Fact]
    public async Task InsertRangeAsync_ChamaRangeAsyncMock()
    {
        // Arrange
        var mockSet = new Mock<DbSet<ArquivoNaoRecepcionado>>();

        // Mock AddRangeAsync para simplesmente retornar Task.CompletedTask
        mockSet.Setup(m => m.AddRangeAsync(It.IsAny<IEnumerable<ArquivoNaoRecepcionado>>(), It.IsAny<CancellationToken>()))
               .Returns(Task.CompletedTask);

        var mockContext = new Mock<IAppDbContext>();
        mockContext.Setup(c => c.ArquivoNaoRecepcionados).Returns(mockSet.Object);
        mockContext.Setup(c => c.Set<ArquivoNaoRecepcionado>()).Returns(mockSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var repository = new ArquivoNaoRecepcionadoRepository(mockContext.Object);

        var arquivos = new List<ArquivoNaoRecepcionado>
    {
        new ArquivoNaoRecepcionado
        {
            DataInsercao = new DateTime(2025, 11, 10),
            Motivos = "A empresa informada não existe em nossa base.",
            EstruturaImportada = "12019052632165487TestEmpresaN0002451"
        },
        new ArquivoNaoRecepcionado
        {
            DataInsercao = new DateTime(2025, 11, 11),
            Motivos = "Data processamento inválida.",
            EstruturaImportada = "09875643219999999920190625201906250000001UfCard"
        }
    };

        // Act
        var result = await repository.InsertRangeAsync(arquivos);

        // Assert
        mockSet.Verify(m => m.AddRangeAsync(arquivos, It.IsAny<CancellationToken>()), Times.Once);
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(arquivos, result);
    }
}
