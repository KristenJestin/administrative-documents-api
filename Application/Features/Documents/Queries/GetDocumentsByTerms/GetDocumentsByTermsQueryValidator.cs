using Application.Parameters;
using FluentValidation;

namespace Application.Features.Documents.Queries.GetDocumentsByTerms
{
    public class GetDocumentsByTermsQueryValidator : PagedRequestQueryValidator<GetDocumentsByTermsQuery>
    {
        public GetDocumentsByTermsQueryValidator()
        {
            // term
            RuleFor(d => d.Term)
                .MinimumLength(2).WithMessage("{PropertyName} must have {MinLength} character.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            When(d => d.Type == null && d.Tag == null, () => {
                RuleFor(d => d.Term)
                    .NotEmpty().WithMessage("At least one of the search fields must be filled in.");
            });
        }
    }
}
