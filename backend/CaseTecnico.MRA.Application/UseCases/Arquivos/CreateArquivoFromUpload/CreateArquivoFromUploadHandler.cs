
using AutoMapper;
using CaseTecnico.MRA.Application.Common.Resources;
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;
using FluentValidation;

namespace CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;

public class CreateArquivoFromUploadHandler
{
    private readonly IMapper _mapper;
    private readonly IArquivoRepository _repository;
    private readonly IValidator<CreateArquivoFromUploadLineDto> _validator;

    public CreateArquivoFromUploadHandler(
        IMapper mapper, 
        IArquivoRepository repository,
        IValidator<CreateArquivoFromUploadLineDto> validator)
    {
        _mapper = mapper;
        _repository = repository;
        _validator = validator;
    }

    public async Task<CreateArquivoFromUploadResponse> Handle(
        CreateArquivoFromUploadRequest request, 
        CancellationToken cancellationToken = default)
    {
        var response = new CreateArquivoFromUploadResponse();
        var linhasDto = new List<CreateArquivoFromUploadLineDto>();

        var ext = Path.GetExtension(request.FileName).ToLower();

        //VALIDAÇÕES CRUCIAIS PARA VERIFICAR ANTES DE LER LINHA POR LINHA.
        if (!ext.Equals(".txt"))
        {
            response.Sucesso = false;
            response.Mensagens.Add(ValidationMessages.ArquivoImpDeveConterExtensaoTXT);
            return response;
        }

        if (request.ArquivoStream == null || request.ArquivoStream.Length == 0)
        {
            response.Sucesso = false;
            response.Mensagens.Add(ValidationMessages.ArquivoImpSemInfo);
            return response;
        }

        using var reader = new StreamReader(request.ArquivoStream);
        int sequencia = 1;

        while (!reader.EndOfStream)
        {
            var linha = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(linha))
            {
                sequencia++;
                continue;
            }

            bool isFagammondCard = linha.Contains("FagammonCard");
            bool isUfCard = linha.Contains("UfCard");

            //VALIDANDO SE EXISTE AS EMPRESAS/FORMATO NO ARQUIVO
            if(isFagammondCard)
            {
                if (linha.Length != 36)
                {
                    response.Mensagens.Add(
                        string.Format(ValidationMessages.ArquivoImpLinhaEmpresaIncorretaQtd,
                        sequencia, "FagammonCard", 36)
                    );
                    sequencia++;
                    continue; // Pula essa linha
                }
            }
            else if (isUfCard)
            {
                if (linha.Length != 47)
                {
                    response.Mensagens.Add(
                        string.Format(ValidationMessages.ArquivoImpLinhaEmpresaIncorretaQtd,
                        sequencia, "UfCard", 47)
                    );
                    sequencia++;
                    continue; // Pula essa linha
                }
            }
            else
            {
                response.Mensagens.Add(
                   string.Format(ValidationMessages.ArquivoImpLinhaEmpresaInexistente, sequencia)
               );
                continue;
            }

            var linhaDto = linha.Contains("UfCard")
                ? ParseUfCard(linha, sequencia, response)
                : ParseFagammonCard(linha, sequencia, response);


            var validationResult = await _validator.ValidateAsync(linhaDto);
            if (!validationResult.IsValid)
            {
                response.Mensagens.AddRange(validationResult.Errors
                    .Select(e => $"Linha {sequencia}: {e.ErrorMessage}"));
            }

            linhasDto.Add(linhaDto);
            sequencia++;
        }

        if(response.Mensagens.Any())
        {
            response.Sucesso = false;
            return response;
        }

        var modelMapper = _mapper.Map<List<Arquivo>>(linhasDto);

        await _repository.InsertRangeAsync(modelMapper, cancellationToken);
        
        return response;
    }

    private CreateArquivoFromUploadLineDto ParseFagammonCard(
        string linha, 
        int sequencia, 
        CreateArquivoFromUploadResponse response)
    {
        int tipoRegistroLine = int.Parse(linha.Substring(0, 1));
        long estabelecimentoLine = long.Parse(linha.Substring(9, 8));
        long sequenciaLine = long.Parse(linha.Substring(29, 7));
        string empresaLine = linha.Substring(17, 12).Trim();
        string dtProcLine = linha.Substring(1, 8);

        var item = new CreateArquivoFromUploadLineDto
        {
            SequenciaLinha = sequencia,
            EstruturaImportada = linha,
            Estabelecimento = estabelecimentoLine,
            Empresa = empresaLine,
            Sequencia = sequenciaLine
        };

        if (DateTime.TryParseExact(dtProcLine, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var dtProcessamento))
            item.DataProcessamento = dtProcessamento;
        else
            response.Mensagens.Add($"Linha {sequencia}: DataProcessamento inválida");

        return item;
    }

    private CreateArquivoFromUploadLineDto ParseUfCard(
        string linha, 
        int sequencia,
        CreateArquivoFromUploadResponse response)
    {

        int tipoRegistroLine = int.Parse(linha.Substring(0, 1));
        long estabelecimentoLine = long.Parse(linha.Substring(1, 9));
        long sequenciaLine = long.Parse(linha.Substring(34, 7));
        string empresaLine = linha.Substring(41, 6).Trim();
        string dtProcLine = linha.Substring(10, 8);
        string dtPIniLine = linha.Substring(18, 8);
        string dtPFinLine = linha.Substring(26, 8);

        var item = new CreateArquivoFromUploadLineDto
        {
            SequenciaLinha = sequencia,
            EstruturaImportada = linha,
            Estabelecimento = estabelecimentoLine,
            Sequencia = sequenciaLine,
            Empresa = empresaLine
        };

        if (DateTime.TryParseExact(dtProcLine, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var dtProcessamento))
            item.DataProcessamento = dtProcessamento;
        else
            response.Mensagens.Add($"Linha {sequencia}: DataProcessamento inválida");

        if (DateTime.TryParseExact(dtPIniLine, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var dtInicial))
            item.PeriodoInicial = dtInicial;
        else
            response.Mensagens.Add(
                string.Format(ValidationMessages.ArquivoImpLinhaCampoInvalido, sequencia, "Periodo Inicial")
            );

        if (DateTime.TryParseExact(dtPFinLine, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var dtFinal))
            item.PeriodoFinal = dtFinal;
        else
            response.Mensagens.Add(
                string.Format(ValidationMessages.ArquivoImpLinhaCampoInvalido, sequencia, "Periodo Final")
            );

        return item;
    }

}
