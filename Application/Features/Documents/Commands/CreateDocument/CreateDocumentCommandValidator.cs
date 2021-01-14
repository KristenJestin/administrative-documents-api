using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Documents.Commands.CreateDocument
{
    public partial class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
    {
        private readonly IDocumentRepositoryAsync documentRepository;

        public CreateDocumentCommandValidator(IDocumentRepositoryAsync documentRepository)
        {
            this.documentRepository = documentRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed {value} characters.");

            RuleFor(p => p.Type)
                .NotNull().WithMessage("{PropertyName} is required.");

            //RuleFor(p => p.File)
            //    .NotNull().WithMessage("{PropertyName} is required.");

            // TODO: add more rule (like file size, file type, etc)
        }

        //private async Task<bool> IsUniqueBarcode(string barcode, CancellationToken cancellationToken)
        //{
        //    return await documentRepository.IsUniqueBarcodeAsync(barcode);
        //}
    }
}
