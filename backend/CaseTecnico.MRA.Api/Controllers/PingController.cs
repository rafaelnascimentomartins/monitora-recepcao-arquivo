using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseTecnico.MRA.Api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
[AllowAnonymous]
public class PingController : ControllerBase
{

    /// <summary>
    ///Health Check da plataform.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param></param>
    /// <returns>Retorna mensagem de Ok para conexão com Api.</returns>
    [HttpGet("/ping")]
    public IActionResult Get()
    {
        return Ok("APLICAÇÃO ONLINE");
    }
}
