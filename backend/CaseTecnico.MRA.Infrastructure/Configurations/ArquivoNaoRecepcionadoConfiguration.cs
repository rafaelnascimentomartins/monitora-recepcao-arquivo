
using CaseTecnico.MRA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseTecnico.MRA.Infrastructure.Configurations;

internal class ArquivoNaoRecepcionadoConfiguration : BaseEntityConfiguration<ArquivoNaoRecepcionado>
{
    public override void Configure(EntityTypeBuilder<ArquivoNaoRecepcionado> builder)
    {
        base.Configure(builder);

        builder.ToTable("ArquivoNaoRecepcionados");

        builder.Property(x => x.Motivos)
             .IsRequired();

        builder.Property(x => x.EstruturaImportada)
            .HasMaxLength(50)
            .IsRequired();
    }
}