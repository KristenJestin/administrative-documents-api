using Application.Interfaces.Repositories;
using Application.Parameters;
using FluentValidation;

namespace Application.Features.Documents.Queries.GetDocumentsByTerms
{
    public class GetDocumentsByTermsQueryValidator : PagedRequestQueryValidator<GetDocumentsByTermsQuery>
    {
        private readonly IDocumentTypeRepositoryAsync _documentTypeRepository;
        private readonly IDocumentTagRepositoryAsync _documentTagRepository;

        public GetDocumentsByTermsQueryValidator(IDocumentTypeRepositoryAsync documentTypeRepository, IDocumentTagRepositoryAsync documentTagRepository)
        {
            #region injections 
            _documentTypeRepository = documentTypeRepository;
            _documentTagRepository = documentTagRepository;
            #endregion

            // term
            RuleFor(d => d.Term)
                .MinimumLength(2).WithMessage("{PropertyName} must have {MinLength} character.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            // TODO: add type and tag
        }
    }
}
