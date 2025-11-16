
using CaseTecnico.MRA.Application.Common.Resources;
using CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;
using FluentValidation;

namespace CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivo;

public class CreateArquivoFromUploadValidator : AbstractValidator<CreateArquivoFromUploadDto>
{
    public CreateArquivoFromUploadValidator()
    {
        RuleFor(x => x.DataProcessamento)
           .NotEmpty().WithMessage(
                string.Format(ValidationMessages.CampoInvalido, "Data processamento")
            )
           .Must(BeAValidDate).WithMessage(
                string.Format(ValidationMessages.CampoInvalido, "Data processamento")
            );

        RuleFor(x => x.PeriodoInicial)
            .Must(BeAValidDateNullable).WithMessage(
                string.Format(ValidationMessages.CampoInvalido, "Periodo inicial")
            );

        RuleFor(x => x.PeriodoFinal)
            .Must(BeAValidDateNullable).WithMessage(
                string.Format(ValidationMessages.CampoInvalido, "Periodo final")
            );

        RuleFor(x => x.Estabelecimento)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.CampoObrigatorio, "Estabelecimento"))
            .MaximumLength(20).WithMessage(string.Format(ValidationMessages.CampoTamanhoMaxInvalido, "Estabelecimento", 20));

        RuleFor(x => x.Sequencia)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.CampoObrigatorio, "Sequencia"))
            .MaximumLength(20).WithMessage(string.Format(ValidationMessages.CampoTamanhoMaxInvalido, "Sequencia", 20));

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
