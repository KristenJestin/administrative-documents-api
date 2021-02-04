using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Parameters
{
    public class PagedRequestQueryValidator<T> : AbstractValidator<T> where T : PagedRequestQuery
    {
        public PagedRequestQueryValidator()
        {
            RuleFor(d => d.Page)
                .GreaterThan(0).WithMessage("{PropertyName} can't be less than 1.")
                .LessThan(1000).WithMessage("{PropertyName} cannot exceed {ComparisonValue}.");

            RuleFor(d => d.PageSize)
                .GreaterThan(0).WithMessage("{PropertyName} can't be less than 1.")
                .LessThanOrEqualTo(10).WithMessage("{PropertyName} cannot exceed {ComparisonValue}.");
        }
    }
}
