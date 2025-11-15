
using CaseTecnico.MRA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseTecnico.MRA.Infrastructure.Configurations;

public sealed class EmpresaConfiguration : BaseEntityConfiguration<Empresa>
{
    public override void Configure(EntityTypeBuilder<Empresa> builder)
    {
        base.Configure(builder);

        builder.ToTable("Empresas");

        builder.Property(x => x.Descricao)
               .HasMaxLength(120)
               .IsRequired();

        //INICIALIZANDO ALGUNS VALORES NA BASE
        builder.HasData(
           new Empresa
           {
               Identificador = Guid.Parse("3a86fd10-725f-4143-ae55-7f127bc85a4e"),
               Descricao = "UfCard",
               DataInsercao = new DateTime(2025, 11, 18)
           },
           new Empresa
           {
               Identificador = Guid.Parse("73c71cf2-2681-40d8-ab7f-ceb0b65aaf86"),
               Descricao = "FagammonCard",
               DataInsercao = new DateTime(2025, 11, 18)
           }
       );
    }
}
