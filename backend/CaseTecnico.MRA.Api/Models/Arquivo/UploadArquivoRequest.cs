using Microsoft.AspNetCore.Mvc;

namespace CaseTecnico.MRA.Api.Models.Arquivo;

public class UploadArquivoRequest
{
    [FromForm(Name = "arquivo")]
    public IFormFile Arquivo { get; set; } = null!;
}
