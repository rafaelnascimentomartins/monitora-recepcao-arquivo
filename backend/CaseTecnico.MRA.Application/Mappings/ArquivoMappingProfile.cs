
using AutoMapper;
using CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;
using CaseTecnico.MRA.Application.UseCases.Arquivos.GetDatatableArquivo;
using CaseTecnico.MRA.Domain.Entities;

namespace CaseTecnico.MRA.Application.Mappings;

public class ArquivoMappingProfile : Profile
{
    public ArquivoMappingProfile()
    {
        CreateMap<CreateArquivoFromUploadLineDto, Arquivo>();
        CreateMap<Arquivo, GetArquivoDatatableResponse>();
    }
}
