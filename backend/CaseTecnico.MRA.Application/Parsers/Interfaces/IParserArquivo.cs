using CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;
using CaseTecnico.MRA.Domain.Entities;

namespace CaseTecnico.MRA.Application.Parsers.Interfaces;

public interface IParserArquivo
{
    Task<CreateArquivoFromUploadParserDto> ParseAsync(string linha, int sequencia, Dictionary<string, Empresa> empresasNoTrack);
}
