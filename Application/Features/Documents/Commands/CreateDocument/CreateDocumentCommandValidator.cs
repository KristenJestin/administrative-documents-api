using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentValidation;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Documents.Commands.CreateDocument
{
    public class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
    {
        private readonly IDocumentRepositoryAsync _documentRepository;
        private readonly IDocumentTypeRepositoryAsync _documentTypeRepository;
        private DocumentType documentType;

        public CreateDocumentCommandValidator(IDocumentRepositoryAsync documentRepository, IDocumentTypeRepositoryAsync documentTypeRepository)
        {
            _documentRepository = documentRepository;
            _documentTypeRepository = documentTypeRepository;

            // name
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            // type
            When(d => d.Type != null, () =>
            {
                // check exist
                RuleFor(d => d.Type)
                    .MustAsync(IsTypeExists).WithMessage("{PropertyName} not exists.")
                    .DependentRules(() =>
                    {
                        // check date
                        When((d) => documentType.HasDate, () =>
                        {
                            RuleFor(d => d.Date).Cascade(CascadeMode.Stop)
                                .NotNull().WithMessage("{PropertyName} is required.")
                                .LessThanOrEqualTo(DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59)).WithMessage("{PropertyName} must be less than today.")
                                .GreaterThan(new DateTime(1970, 1, 1)).WithMessage("{PropertyName} must be greater.");
                        });

                        // check amount
                        When((d) => documentType.HasAmount, () =>
                        {
                            RuleFor(d => d.Amount).Cascade(CascadeMode.Stop)
                                .NotNull().WithMessage("{PropertyName} is required.")
                                .GreaterThan(0).WithMessage("{PropertyName} is not a valid currency.");
                        });

                        // check date
                        When((d) => documentType.HasDuration, () =>
                        {
                            RuleFor(d => d.Duration).Cascade(CascadeMode.Stop)
                                .NotNull().WithMessage("{PropertyName} is required.")
                                .GreaterThan(0).WithMessage("{PropertyName} must be at least 1 month.")
                                .LessThan(1200).WithMessage("{PropertyName} must be shorter.");
                        });
                    });
            });

            // file
            RuleFor(d => d.File)
                .NotNull().WithMessage("{PropertyName} is required.")
                .DependentRules(() =>
                {
                    RuleFor(d => d.File.Length)
                        .NotNull()
                        .GreaterThan(0).WithMessage("{PropertyName} size must be greater than {ComparisonValue}.")
                        .LessThanOrEqualTo(52428800).WithMessage("{PropertyName} size is larger than allowed (50MB max).");
                    
                    RuleFor(d => d.File.ContentType)
                        .NotNull()
                        .Must(IsValidFileType).WithMessage(d => $"{Path.GetExtension(d.File.FileName)} type is not allowed.");
                });

            // note
            RuleFor(d => d.Note)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

        }


        #region privates
        private async Task<bool> IsTypeExists(int? id, CancellationToken cancellationToken)
            => id != null && id > 0 ? (await GetDocumentTypeAsync(id.Value)) != null : false;
        public async Task<DocumentType> GetDocumentTypeAsync(int id)
            => documentType ??= await _documentTypeRepository.FindByIdAsync(id);

        private bool IsValidFileType(string type)
            => CreateDocumentCommand.ALLOWED_FILE_TYPES.Contains(type);
        #endregion
    }
}
