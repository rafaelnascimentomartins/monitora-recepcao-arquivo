using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;
using CaseTecnico.MRA.Infrastructure.Context;
using System;
using System.Collections.Generic;

namespace CaseTecnico.MRA.Infrastructure.Repositories;

public class ArquivoRepository : BaseRepository<Arquivo>, IArquivoRepository
{
    public ArquivoRepository(AppDbContext context)
           : base(context)
    {
    }
}
