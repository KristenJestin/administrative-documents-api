using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Behaviours
{
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			_validators = validators;
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			if (_validators.Any())
			{
				ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
				ValidationResult[] validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
				List<ValidationFailure> failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

				ModelStateDictionary modelState = new ModelStateDictionary();
				foreach (ValidationFailure failure in failures)
				{
					modelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
				}

				if (failures.Count != 0)
					throw new ApiProblemDetailsException(modelState);
			}

			return await next();
		}
	}
}
