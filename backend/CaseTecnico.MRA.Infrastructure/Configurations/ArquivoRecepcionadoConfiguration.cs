
using CaseTecnico.MRA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseTecnico.MRA.Infrastructure.Configurations;

public class ArquivoRecepcionadoConfiguration : BaseEntityConfiguration<ArquivoRecepcionado>
{
    public override void Configure(EntityTypeBuilder<ArquivoRecepcionado> builder)
    {
        base.Configure(builder);

        builder.ToTable("ArquivoRecepcionados");

        builder.Property(x => x.Estabelecimento)
             .HasMaxLength(20)
             .IsRequired();

        builder.Property(x => x.Sequencia)
             .HasMaxLength(20)
             .IsRequired();

        builder.Property(x => x.PeriodoFinal)
               .HasColumnType("datetime2(3)")
               .IsRequired(false);

        builder.Property(x => x.PeriodoInicial)
               .HasColumnType("datetime2(3)")
               .IsRequired(false);

        builder.Property(x => x.DataProcessamento)
               .HasColumnType("datetime2(3)")
               .IsRequired();

        builder.Property(x => x.EstruturaImportada)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasOne(x => x.Empresa)
              .WithMany(e => e.ArquivoRecepcionados) // COLLECTION Arquivos dentro da classe para busca inversa (se necessário)
              .HasForeignKey(x => x.EmpresaId)
              .IsRequired();

    }
}