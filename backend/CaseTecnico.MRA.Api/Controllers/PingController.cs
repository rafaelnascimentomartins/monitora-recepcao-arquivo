using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseTecnico.MRA.Api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class PingController : ControllerBase
{
    [AllowAnonymous, HttpGet]
    public IActionResult Get()
    {
        return Ok("APLICAÇÃO ONLINE");
    }
}
