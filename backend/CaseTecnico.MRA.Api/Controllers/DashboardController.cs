using CaseTecnico.MRA.Application.UseCases.Dashboards.GetDashArquivoResumoStatus;
using Microsoft.AspNetCore.Mvc;

namespace CaseTecnico.MRA.Api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly GetDashArquivoResumoStatusHandler _getDashArquivoResumoStatusHandler;

    public DashboardController(GetDashArquivoResumoStatusHandler getDashArquivoResumoStatusHandler)
    {
        _getDashArquivoResumoStatusHandler = getDashArquivoResumoStatusHandler;
    }

    /// <summary>
    /// Método utilizado para realizar a consulta via dashboard FronEnd. 
    /// </summary>
    /// <remarks>
    /// Este endpoint não possui parâmetros.
    /// 
    /// Acesso via Postman: Headers -> appsettings config header: X-API-KEY e X-API-SECRET
    /// </remarks>
    /// <returns>Retorna sucesso com a lista padronizada para dashboard.</returns>
    [HttpGet]
    public async Task<IActionResult> GetResumoStatus(CancellationToken cancellationToken)
    {
        return Ok(await _getDashArquivoResumoStatusHandler.Handle(cancellationToken));
    }
}
