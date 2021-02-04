using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Documents.Queries.GetDocumentsByTerms
{
    public class GetDocumentsByTermsValidator : AbstractValidator<GetDocumentsByTermsParameter>
    {
        private readonly IDocumentTypeRepositoryAsync _documentTypeRepository;
        private readonly IDocumentTagRepositoryAsync _documentTagRepository;

        public GetDocumentsByTermsValidator(IDocumentTypeRepositoryAsync documentTypeRepository, IDocumentTagRepositoryAsync documentTagRepository)
        {
            _documentTypeRepository = documentTypeRepository;
            _documentTagRepository = documentTagRepository;

            // term
            RuleFor(d => d.Term)
                .MinimumLength(1).WithMessage("{PropertyName} must have {MinLength} character.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            // TODO: add type and tag
        }
    }
}
