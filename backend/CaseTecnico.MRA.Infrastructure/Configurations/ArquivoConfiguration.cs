using CaseTecnico.MRA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseTecnico.MRA.Infrastructure.Configurations;

public sealed class ArquivoConfiguration : BaseEntityConfiguration<Arquivo>
{
    public override void Configure(EntityTypeBuilder<Arquivo> builder)
    {
        base.Configure(builder);

        builder.ToTable("Arquivos");

        builder.Property(x => x.Estabelecimento)
             .IsRequired();

        builder.Property(x => x.Sequencia)
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

        builder.HasOne(x => x.ArquivoStatus)
       .WithMany(s => s.Arquivos)  // COLLECTION Arquivos dentro da classe para busca inversa (se necessário)
       .HasForeignKey(x => x.ArquivoStatusId)
       .IsRequired();

        builder.HasOne(x => x.Empresa)
               .WithMany(e => e.Arquivos) // COLLECTION Arquivos dentro da classe para busca inversa (se necessário)
               .HasForeignKey(x => x.EmpresaId)
               .IsRequired();
    }
}
