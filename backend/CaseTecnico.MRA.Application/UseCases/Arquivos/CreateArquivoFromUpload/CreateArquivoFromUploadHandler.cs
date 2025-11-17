
using AutoMapper;
using CaseTecnico.MRA.Application.Common.Resources;
using CaseTecnico.MRA.Application.Interfaces.Parsers;
using CaseTecnico.MRA.Application.Settings;
using CaseTecnico.MRA.CrossCutting.Interfaces.Services;
using CaseTecnico.MRA.Domain.Entities;
using CaseTecnico.MRA.Domain.Interfaces.Repositories;
using FluentValidation;
using System.Dynamic;

namespace CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;

public class CreateArquivoFromUploadHandler
{
    private readonly IMapper _mapper;
    private readonly IArquivoRecepcionadoRepository _arquivoRecepcionadoRepo;
    private readonly IArquivoNaoRecepcionadoRepository _arquivoNaoRecepcionadoRepo;
    private readonly IEmpresaRepository _empresaRepo;
    private readonly IValidator<CreateArquivoFromUploadRecepcionadoDto> _validator;
    private readonly IFileEncryptionService _fileService;
    private readonly IParserArquivo _parse;
    private readonly AppSettings _settings;
    private Dictionary<string, Empresa>? empresasNoTrack;

    public CreateArquivoFromUploadHandler(
        IMapper mapper,
        IArquivoRecepcionadoRepository arquivoRecepcionadoRepo,
        IArquivoNaoRecepcionadoRepository arquivoNaoRecepcionadoRepo,
        IEmpresaRepository empresaRepo,
        IFileEncryptionService fileService,
        IParserArquivo parse,
        IValidator<CreateArquivoFromUploadRecepcionadoDto> validator,
        AppSettings settings)
    {
        _mapper = mapper;
        _arquivoRecepcionadoRepo = arquivoRecepcionadoRepo;
        _arquivoNaoRecepcionadoRepo = arquivoNaoRecepcionadoRepo;
        _empresaRepo = empresaRepo;
        _validator = validator;
        _fileService = fileService;
        _settings = settings;
        _parse = parse;
    }

    public async Task<CreateArquivoFromUploadResponse> Handle(
        CreateArquivoFromUploadRequest request, 
        CancellationToken cancellationToken = default)
    {
        var response = new CreateArquivoFromUploadResponse();
        var linhasDto = new CreateArquivoFromUploadDto();
        
        var ext = Path.GetExtension(request.FileName).ToLower();

       //VALIDAÇÕES CRUCIAIS PARA VERIFICAR ANTES DE LER LINHA POR LINHA.
        if (!ext.Equals(".txt"))
        {
            response.Sucesso = false;
            response.Mensagens.Add(ValidationMessages.ArquivoImpDeveConterExtensaoTXT);
            return response;
        }
        else if (request.ArquivoStream == null || request.ArquivoStream.Length == 0)
        {
            response.Sucesso = false;
            response.Mensagens.Add(ValidationMessages.ArquivoImpSemInfo);
            return response;
        }

        //BUSCANDO EMPRESAS NA BASE, PARA BUSCA DO IDENTIDICADOR
        //NÃO POSSUI MUITAS EMPRESAS CADASTRADAS
        empresasNoTrack = await _empresaRepo.GetAsDictionaryAsync(x => x.Descricao);

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

            var resultadoLinha = await _parse.ParseAsync(linha!, sequencia, empresasNoTrack);

            if (resultadoLinha.Valido)
                linhasDto.Recepcionados.Add(resultadoLinha.Recepcionado!);
            else
                linhasDto.NaoRecepcionados.Add(resultadoLinha.NaoRecepcionado!);

            sequencia++;
        }

        if(linhasDto.NaoRecepcionados.Any())
        {
            var modelMapper = _mapper.Map<List<ArquivoNaoRecepcionado>>(linhasDto.NaoRecepcionados);

            await _arquivoNaoRecepcionadoRepo.InsertRangeAsync(modelMapper, cancellationToken);
        }
        
        if(linhasDto.Recepcionados.Any())
        {
            var modelMapper = _mapper.Map<List<ArquivoRecepcionado>>(linhasDto.Recepcionados);

            await _arquivoRecepcionadoRepo.InsertRangeAsync(modelMapper, cancellationToken);
        }


        // Salvar arquivo encriptado
        await _fileService.SaveEncryptedAsync(
            request.ArquivoStream,
            _settings.FileEncryption.DestinationPath!,
            _settings.FileEncryption.FileKey!);

        return response;
    }
}
