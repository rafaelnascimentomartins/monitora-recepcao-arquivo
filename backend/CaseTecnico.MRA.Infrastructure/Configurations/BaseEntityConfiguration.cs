
using CaseTecnico.MRA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseTecnico.MRA.Infrastructure.Configurations;

public class BaseEntityConfiguration<TEntity> :
    IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Identificador);

        builder.Property(x => x.Identificador)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(x => x.DataInsercao)
               .HasColumnType("datetime2(3)")
               .IsRequired();

        builder.Property(x => x.DataExclusao)
               .HasColumnType("datetime2(3)")
               .IsRequired(false);
    }
}
