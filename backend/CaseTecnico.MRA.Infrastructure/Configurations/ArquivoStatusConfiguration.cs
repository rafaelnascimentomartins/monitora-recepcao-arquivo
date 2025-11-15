using CaseTecnico.MRA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseTecnico.MRA.Infrastructure.Configurations;

public sealed class ArquivoStatusConfiguration : BaseEntityConfiguration<ArquivoStatus>
{
    public override void Configure(EntityTypeBuilder<ArquivoStatus> builder)
    {
        base.Configure(builder);

        builder.ToTable("ArquivoStatus");

        builder.Property(x => x.Descricao)
               .HasMaxLength(20)
               .IsRequired();

        //INICIALIZANDO ALGUNS VALORES NA BASE
        builder.HasData(
           new ArquivoStatus
           {
               Identificador = Guid.Parse("8b44e4f4-2d8b-437f-ad3c-ee0d77ef97b0"),
               Descricao = "Recepcionado",
               DataInsercao = new DateTime(2025, 11, 18)
           },
           new ArquivoStatus
           {
               Identificador = Guid.Parse("d788bcaf-cdb2-495c-985d-355ce1157544"),
               Descricao = "Não Recepcionado",
               DataInsercao = new DateTime(2025, 11, 18)
           }
       );
    }
}
