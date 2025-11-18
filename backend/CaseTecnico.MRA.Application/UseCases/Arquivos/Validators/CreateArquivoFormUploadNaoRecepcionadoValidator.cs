
using CaseTecnico.MRA.Application.Common.Resources;
using CaseTecnico.MRA.Application.UseCases.Arquivos.CreateArquivoFromUpload;
using FluentValidation;

namespace CaseTecnico.MRA.Application.UseCases.Arquivos.Validators;

public class CreateArquivoFormUploadNaoRecepcionadoValidator : AbstractValidator<CreateArquivoFromUploadNaoRecepcionadoDto>
{
    public CreateArquivoFormUploadNaoRecepcionadoValidator()
    {
        RuleFor(x => x.EstruturaImportada)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.ArquivoEstruturaNaoDefinida))
            .MaximumLength(50).WithMessage(string.Format(ValidationMessages.ArquivoEstruturaMaxLength, 50));
    }
}