
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
            .GreaterThan(0).WithMessage("Estabelecimento deve ser maior que zero.");

        RuleFor(x => x.Sequencia)
            .GreaterThan(0).WithMessage("Sequência deve ser maior que zero.");

        RuleFor(x => x.Empresa)
            .NotEmpty().WithMessage("Empresa é obrigatória.")
            .MaximumLength(50).WithMessage("Empresa possui tamanho inválido.");

        RuleFor(x => x.EstruturaImportada)
            .NotEmpty().WithMessage("Estrutura importada é obrigatória.");
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
