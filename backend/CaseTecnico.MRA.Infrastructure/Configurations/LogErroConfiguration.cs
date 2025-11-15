
using CaseTecnico.MRA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseTecnico.MRA.Infrastructure.Configurations;

public sealed class LogErroConfiguration : BaseEntityConfiguration<LogErro>
{
    public override void Configure(EntityTypeBuilder<LogErro> builder)
    {
        base.Configure(builder);

        builder.ToTable("LogErros");

        builder.Property(x => x.Message)
               .HasMaxLength(500)
               .IsRequired();

        builder.Property(x => x.StackTrace)
            .IsRequired(false);
    }
}
