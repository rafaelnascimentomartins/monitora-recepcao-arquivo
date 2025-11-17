using CaseTecnico.MRA.Application.Common.Resources;
using CaseTecnico.MRA.Application.Interfaces.Parsers;
using CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;
using CaseTecnico.MRA.CrossCutting.Utils;
using CaseTecnico.MRA.Domain.Entities;
using FluentValidation;

namespace CaseTecnico.MRA.Application.Parsers;

public class ParserArquivo : IParserArquivo
{
    private readonly IValidator<CreateArquivoFromUploadRecepcionadoDto> _validator;

    public ParserArquivo(IValidator<CreateArquivoFromUploadRecepcionadoDto> validator)
    {
        _validator = validator;
    }

    public async Task<CreateArquivoFromUploadParserDto> ParseAsync(string linha, int sequencia, Dictionary<string, Empresa> empresasNoTrack)
    {
        var resultado = new CreateArquivoFromUploadParserDto();

        bool isFagammondCard = linha.Contains("FagammonCard");
        bool isUfCard = linha.Contains("UfCard");

        CreateArquivoFromUploadNaoRecepcionadoDto linhaNotOk = new()
        {
            EstruturaImportada = linha
        };


        if (!isFagammondCard && !isUfCard)
            linhaNotOk.AddNewMotivo(ValidationMessages.ArquivoImpEmpresaInexistente);

        if (isFagammondCard && linha.Length != 36)
            linhaNotOk.AddNewMotivo(
                string.Format(ValidationMessages.ArquivoImpEmpresaIncorretaQtd, "FagammonCard", 36
                )
             );

        if (isUfCard && linha.Length != 47)
            linhaNotOk.AddNewMotivo(
               string.Format(ValidationMessages.ArquivoImpEmpresaIncorretaQtd, "UfCard", 47
               )
            );

        // Se já há motivos, retorna direto
        if (!string.IsNullOrEmpty(linhaNotOk.Motivos))
        {
            resultado.NaoRecepcionado = linhaNotOk;
            return resultado;
        }

        // Parsing real
        string erroLines = string.Empty;
        var linhaOk = isFagammondCard
           ? ParseFagammonCard(linha, sequencia, empresasNoTrack, out erroLines)
           : ParseUfCard(linha, sequencia, empresasNoTrack, out erroLines);

        if (!string.IsNullOrEmpty(erroLines))
            linhaNotOk.AddNewMotivo(erroLines);
        else
        {
            var validationResult = await _validator.ValidateAsync(linhaOk);
            if (!validationResult.IsValid)
            {
                foreach (var e in validationResult.Errors)
                    linhaNotOk.AddNewMotivo(e.ErrorMessage);
            }
        }

        if (linhaNotOk.Motivos != null)
            resultado.NaoRecepcionado = linhaNotOk;
        else
            resultado.Recepcionado = linhaOk;

        return resultado;
    }

    private CreateArquivoFromUploadRecepcionadoDto ParseFagammonCard(
    string linha,
    int sequencia,
    Dictionary<string, Empresa> empresasNoTrack,
    out string motivoErro)
    {
        motivoErro = string.Empty;

        string estabelecimentoLine = linha.Substring(9, 8);
        string sequenciaLine = linha.Substring(29, 7);
        string empresaLine = linha.Substring(17, 12).Trim();
        string dtProcLine = linha.Substring(1, 8);

        if (!empresasNoTrack.ContainsKey(empresaLine))
        {
            motivoErro = motivoErro.AdicionarMotivo(
                string.Format(ValidationMessages.EmpresaInexistenteNaBase, empresaLine)
            );
        }

        var item = new CreateArquivoFromUploadRecepcionadoDto
        {
            EstruturaImportada = linha,
            Estabelecimento = estabelecimentoLine,
            Sequencia = sequenciaLine,
            EmpresaId = empresasNoTrack[empresaLine].Identificador
        };

        if (!DateTime.TryParseExact(dtProcLine, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var dtProcessamento))
            motivoErro = motivoErro.AdicionarMotivo(
               string.Format(ValidationMessages.CampoInvalido, "Data Processamento")
           );
        else
            item.DataProcessamento = dtProcessamento;

        return item;
    }

    private CreateArquivoFromUploadRecepcionadoDto ParseUfCard(
    string linha,
    int sequencia,
    Dictionary<string, Empresa> empresasNoTrack,
    out string motivoErro)
    {
        motivoErro = string.Empty;

        string estabelecimentoLine = linha.Substring(1, 9);
        string sequenciaLine = linha.Substring(34, 7);
        string empresaLine = linha.Substring(41, 6).Trim();
        string dtProcLine = linha.Substring(10, 8);
        string dtPIniLine = linha.Substring(18, 8);
        string dtPFinLine = linha.Substring(26, 8);

        if (!empresasNoTrack.ContainsKey(empresaLine))
        {
            motivoErro = motivoErro.AdicionarMotivo(
                string.Format(ValidationMessages.EmpresaInexistenteNaBase, empresaLine)
            );
        }

        var item = new CreateArquivoFromUploadRecepcionadoDto
        {
            EstruturaImportada = linha,
            Estabelecimento = estabelecimentoLine,
            Sequencia = sequenciaLine,
            EmpresaId = empresasNoTrack[empresaLine].Identificador
        };

        if (DateTime.TryParseExact(dtProcLine, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var dtProcessamento))
            item.DataProcessamento = dtProcessamento;
        
        if (DateTime.TryParseExact(dtPIniLine, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var dtInicial))
            item.PeriodoInicial = dtInicial;
        else
            motivoErro = motivoErro.AdicionarMotivo(
                string.Format(ValidationMessages.CampoInvalido, "Período Inicial")
            );

        if (DateTime.TryParseExact(dtPFinLine, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var dtFinal))
            item.PeriodoFinal = dtFinal;
        else
            motivoErro = motivoErro.AdicionarMotivo(
              string.Format(ValidationMessages.CampoInvalido, "Período Final")
          );

        return item;
    }

}

