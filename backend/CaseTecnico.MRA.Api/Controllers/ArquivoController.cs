using CaseTecnico.MRA.Api.Models.Arquivo;
using CaseTecnico.MRA.Api.Resources;
using CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;
using CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDashResumoStatus;
using CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDatatable;
using CaseTecnico.MRA.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CaseTecnico.MRA.Api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class ArquivoController : ControllerBase
{
    private readonly CreateArquivoFromUploadHandler _createArquivoFromUploadHandler;
    private readonly GetArquivoDatatableHandler _getArquivoDatatableHandler;
    private readonly GetArquivoDashResumoStatusHandler _getArquivoDashResumoStatusHandler;

    public ArquivoController(
        CreateArquivoFromUploadHandler createArquivoFromUploadHandler,
        GetArquivoDatatableHandler getArquivoDatatableHandler,
        GetArquivoDashResumoStatusHandler getArquivoDashResumoStatusHandler)
    {
        _createArquivoFromUploadHandler = createArquivoFromUploadHandler;
        _getArquivoDatatableHandler = getArquivoDatatableHandler;
        _getArquivoDashResumoStatusHandler = getArquivoDashResumoStatusHandler;
    }

    /// <summary>
    /// Método utilizado para realizar o download do template para uso correto do método "Upload",
    /// </summary>
    /// <remarks>
    /// Este endpoint recebe não possui parâmetros.
    /// 
    /// Acesso via Postman: Headers -> appsettings config header: X-API-KEY e X-API-SECRET
    /// </remarks>
    /// <returns>Retorna um File .txt</returns>
    [HttpGet]
    [Produces("text/plain")]
    public IActionResult DownloadTemplateUpload()
    {
        // Caminho ou memória do arquivo exemplo
        var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "template_upload_arquivo.txt");

        if (!System.IO.File.Exists(caminhoArquivo))
            return NotFound();

        var bytes = System.IO.File.ReadAllBytes(caminhoArquivo);

        return File(bytes, "text/plain", "Exemplo.txt");
    }

    /// <summary>
    /// Realiza o upload do arquivo .txt contendo os dados bancários das empresas FagammonCard
    /// e UfCard para cadastro massivo. 
    /// </summary>
    /// <remarks>
    /// Este endpoint recebe como parâmetro um IFormFile dentro de uma classe, valida
    /// se o documento anexado possui a extensão .txt, contém registros e valida o formato.
    /// Após essas análises ele será executa o processo de inserção no banco de dados.
    /// 
    /// Acesso via Postman: Headers -> appsettings config header: X-API-KEY e X-API-SECRET | Body -> form-data Key File
    /// </remarks>
    /// <param name="requestDtoApi">Classe UploadArquivoRequest com único campo arquivo (IFormFile) </param>
    /// <param name="cancellationToken">Configuração interna de cancellationToken </param>
    /// <returns>Retorna sucesso ou lista de validações com mensagens</returns>
    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] UploadArquivoRequest requestDtoApi, CancellationToken cancellationToken)
    {
        if (requestDtoApi is null || requestDtoApi.Arquivo is null || requestDtoApi.Arquivo.Length == 0)
            return BadRequest(ValidationMessages.ArquivoImpVazio);

        using var stream = requestDtoApi.Arquivo.OpenReadStream();
        var request = new CreateArquivoFromUploadRequest
        {
            ArquivoStream = stream,
            FileName = requestDtoApi.Arquivo.FileName
        };

        var result = await _createArquivoFromUploadHandler.Handle(request, cancellationToken);

        return result.Sucesso ? Ok(result) : BadRequest(result.Mensagens);
    }

    /// <summary>
    /// Método utilizado para realizar a consulta via datatable FronEnd e filtragem. 
    /// </summary>
    /// <remarks>
    /// Este endpoint recebe como parâmetro o modelo request contendo os campos de filtragem de 
    /// resultados e este modelo possui uma herença de BaseFilter contendo todas os parâmetros
    /// de configurções do datatable.
    /// 
    /// Acesso via Postman: Headers -> appsettings config header: X-API-KEY e X-API-SECRET
    /// </remarks>
    /// <param name="request">Classe GetArquivoDatatableRequest com os campos de filtragem e com uso de herança do 
    /// BaseFilter contendo os campos de configuração do datatable.
    /// </param>
    /// <param name="cancellationToken">Configuração interna de cancellationToken </param>
    /// <returns>Retorna sucesso com a lista padronizada para datatable.</returns>
    [HttpGet]
    public async Task<IActionResult> GetDatatable([FromQuery] GetArquivoDatatableRequest request, CancellationToken cancellationToken)
    {
        return Ok(await _getArquivoDatatableHandler.Handle(request, cancellationToken));
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
    public async Task<IActionResult> GetDashResumoStatus(CancellationToken cancellationToken)
    {
        return Ok(await _getArquivoDashResumoStatusHandler.Handle(cancellationToken));
    }
}
