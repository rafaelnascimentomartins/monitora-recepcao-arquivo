
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;
using CaseTecnico.MRA.Infrastructure.Context;

namespace CaseTecnico.MRA.Infrastructure.Repositories;

public class LogErroRepository : BaseRepository<LogErro>, ILogErroRepository
{
    public LogErroRepository(AppDbContext context)
        : base(context)
    {
    }


}
