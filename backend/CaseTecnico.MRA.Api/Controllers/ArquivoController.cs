using CaseTecnico.MRA.Api.Models.Arquivo;
using CaseTecnico.MRA.Api.Resources;
using CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;
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

    public ArquivoController(
        CreateArquivoFromUploadHandler createArquivoFromUploadHandler,
        GetArquivoDatatableHandler getArquivoDatatableHandler)
    {
        _createArquivoFromUploadHandler = createArquivoFromUploadHandler;
        _getArquivoDatatableHandler = getArquivoDatatableHandler;
    }

    /// <summary>
    /// Realiza o upload do arquivo .txt contendo os dados bancários das empresas FagammonCard
    /// e UfCard para cadastro massivo. 
    /// </summary>
    /// <remarks>
    /// Este endpoint recebe como parâmetro um IFormFile dentro de uma classe, valida
    /// se o documento anexado possui a extensão .txt, contém registros e valida o formato.
    /// Após essas análises ele será executa o processo de inserção no banco de dados.
    /// </remarks>
    /// <param name="file">Classe UploadArquivoRequest com único campo arquivo (IFormFile) </param>
    /// <returns>Retorna sucesso ou lista de mensagens de erro</returns>
    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] UploadArquivoRequest file, CancellationToken cancellationToken)
    {
        if (file is null || file.Arquivo is null || file.Arquivo.Length == 0)
            return BadRequest(ValidationMessages.ArquivoImpVazio);

        using var stream = file.Arquivo.OpenReadStream();
        var request = new CreateArquivoFromUploadRequest
        {
            ArquivoStream = stream,
            FileName = file.Arquivo.FileName
        };

        var result = await _createArquivoFromUploadHandler.Handle(request, cancellationToken);

        return result.Sucesso ? Ok(result) : BadRequest(result.Mensagens);
    }

    [HttpGet]
    public async Task<IActionResult> GetDatatable(CancellationToken cancellationToken)
    {
        var result = await _getArquivoDatatableHandler.Handle(cancellationToken);

        return Ok();
    }

}
