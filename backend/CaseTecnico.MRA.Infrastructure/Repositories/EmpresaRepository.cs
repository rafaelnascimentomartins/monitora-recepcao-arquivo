
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;
using CaseTecnico.MRA.Infrastructure.Context;

namespace CaseTecnico.MRA.Infrastructure.Repositories;


public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
{
    public EmpresaRepository(IAppDbContext context)
           : base(context)
    {
    }
}