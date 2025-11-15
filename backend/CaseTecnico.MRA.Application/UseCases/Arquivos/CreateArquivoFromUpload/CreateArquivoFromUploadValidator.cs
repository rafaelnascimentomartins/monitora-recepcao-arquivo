
using CaseTecnico.MRA.Application.Common.Resources;
using CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;
using FluentValidation;

namespace CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivo;

public class CreateArquivoFromUploadValidator : AbstractValidator<CreateArquivoFromUploadLineDto>
{
    public CreateArquivoFromUploadValidator()
    {
        RuleFor(x => x.DataProcessamento)
           .NotEmpty().WithMessage("Data de processamento é obrigatória.")
           .Must(BeAValidDate).WithMessage("Data de processamento inválida.");

        RuleFor(x => x.PeriodoInicial)
            .Must(BeAValidDateNullable).WithMessage("Período inicial inválido.");

        RuleFor(x => x.PeriodoFinal)
            .Must(BeAValidDateNullable).WithMessage("Período final inválido.");

        RuleFor(x => x.Estabelecimento)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.CampoObrigatorio, "Estabelecimento"))
            .MaximumLength(20).WithMessage(string.Format(ValidationMessages.CampoTamanhoMaxInvalido, "Estabelecimento", 20));

        RuleFor(x => x.Sequencia)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.CampoObrigatorio, "Sequencia"))
            .MaximumLength(20).WithMessage(string.Format(ValidationMessages.CampoTamanhoMaxInvalido, "Sequencia", 20));

        RuleFor(x => x.Empresa)
            .NotEmpty().WithMessage("Empresa é obrigatória.");

        RuleFor(x => x.EstruturaImportada)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.CampoObrigatorio, "EstruturaObject"))
            .MaximumLength(50).WithMessage(string.Format(ValidationMessages.CampoTamanhoMaxInvalido, "EstruturaObject", 50));
    }

    private bool BeAValidDate(DateTime date)
    {
        return date != default;
    }

    private bool BeAValidDateNullable(DateTime? date)
    {
        return !date.HasValue || date.Value != default;
    }
}
