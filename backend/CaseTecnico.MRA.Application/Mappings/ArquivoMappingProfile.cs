
using AutoMapper;
using CaseTecnico.MRA.Application.UseCases.ArquivoNaoRecepcionados.GetArquivoNaoRecepcionadoDatatable;
using CaseTecnico.MRA.Application.UseCases.ArquivoRecepcionados.GetArquivoRecepcionadoDatatable;
using CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;
using CaseTecnico.MRA.Domain.Entities;

namespace CaseTecnico.MRA.Application.Mappings;

public class ArquivoMappingProfile : Profile
{
    public ArquivoMappingProfile()
    {
        CreateMap<CreateArquivoFromUploadRecepcionadoDto, ArquivoRecepcionado>();


        //ArquivoRecepcionado
        CreateMap<ArquivoRecepcionado, CreateArquivoFromUploadRecepcionadoDto>();
        CreateMap<ArquivoRecepcionado, GetArquivoRecepcionadoDatatableDto>()
            .ForMember(
                dest => dest.EmpresaDescricao,
                opt => opt.MapFrom(src => src.Empresa != null ? src.Empresa.Descricao : null)
            );

        //ArquivoNaoRecepcionado
        CreateMap<CreateArquivoFromUploadNaoRecepcionadoDto, ArquivoNaoRecepcionado>();
        CreateMap<ArquivoNaoRecepcionado, GetArquivoNaoRecepcionadoDatatableDto>();
    }
}
