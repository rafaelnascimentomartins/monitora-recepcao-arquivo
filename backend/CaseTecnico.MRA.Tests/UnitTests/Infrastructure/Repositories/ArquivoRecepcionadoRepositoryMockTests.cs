
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Infrastructure.Context;
using CaseTecnico.MRA.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CaseTecnico.MRA.Tests.UnitTests.Infrastructure.Repositories;

public class ArquivoRecepcionadoRepositoryMockTests
{
    [Fact]
    public async Task InsertRangeAsync_ChamaRangeAsyncMock()
    {
        // Arrange
        var mockSet = new Mock<DbSet<ArquivoRecepcionado>>();

        // Mock AddRangeAsync para simplesmente retornar Task.CompletedTask
        mockSet.Setup(m => m.AddRangeAsync(It.IsAny<IEnumerable<ArquivoRecepcionado>>(), It.IsAny<CancellationToken>()))
               .Returns(Task.CompletedTask);

        var mockContext = new Mock<IAppDbContext>();
        mockContext.Setup(c => c.ArquivoRecepcionados).Returns(mockSet.Object);
        mockContext.Setup(c => c.Set<ArquivoRecepcionado>()).Returns(mockSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var repository = new ArquivoRecepcionadoRepository(mockContext.Object);

        var arquivos = new List<ArquivoRecepcionado>
    {
        new ArquivoRecepcionado
            {
                DataInsercao = new DateTime(2025, 11, 10),
                PeriodoInicial = null,
                PeriodoFinal = null,
                DataProcessamento = new DateTime(2019, 5, 26),
                EmpresaId = new Guid("73C71CF2-2681-40D8-AB7F-CEB0B65AAF86"),
                Sequencia = "0002451",
                Estabelecimento = "32165487",
                EstruturaImportada = "12019052632165487FagammonCard0002451"
            },
            new ArquivoRecepcionado
            {
                DataInsercao = new DateTime(2025, 11, 10),
                PeriodoInicial = new DateTime(2019, 5, 25),
                PeriodoFinal = new DateTime(2019, 5, 25),
                DataProcessamento = new DateTime(2019, 5, 26),
                EmpresaId = new Guid("3A86FD10-725F-4143-AE55-7F127BC85A4E"),
                Sequencia = "0000001",
                Estabelecimento = "987564321",
                EstruturaImportada = "09875643212019062620190625201906250000001UfCard"
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
