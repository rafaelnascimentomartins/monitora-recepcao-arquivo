
using AutoMapper;
using CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;
using CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDashResumoStatus;
using CaseTecnico.MRA.Application.UseCases.Arquivos.GetArquivoDatatable;
using CaseTecnico.MRA.Domain.Common.Models;
using CaseTecnico.MRA.Domain.Entities;

namespace CaseTecnico.MRA.Application.Mappings;

public class ArquivoMappingProfile : Profile
{
    public ArquivoMappingProfile()
    {
        CreateMap<CreateArquivoFromUploadDto, Arquivo>();
        CreateMap<Arquivo, GetArquivoDatatableDto>()
            .ForMember(
                dest => dest.EmpresaDescricao, 
                opt => opt.MapFrom(src => src.Empresa != null ? src.Empresa.Descricao : null)
            )
            .ForMember(
                dest => dest.ArquivoStatusDescricao,
                opt => opt.MapFrom(src => src.ArquivoStatus != null ? src.ArquivoStatus.Descricao : null)
            );
        CreateMap<ArquivoResumoStatusModel, GetArquivoDashResumoStatusDto>();
    }
}
