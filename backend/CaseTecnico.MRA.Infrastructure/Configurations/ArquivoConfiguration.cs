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
    }
}
