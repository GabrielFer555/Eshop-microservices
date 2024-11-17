using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviours
{
	public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
		: IPipelineBehavior<TRequest, TResponse>
		where TRequest : ICommand<TResponse>
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			var context = new ValidationContext<TRequest>(request);
			//complete all validation operations
			var validationResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));
			//check for validation errors

			var failures =
				validationResults.Where(x => x.Errors.Any()).
				SelectMany(x => x.Errors).ToList();

			if (failures.Any()) {
				throw new ValidationException(failures);
			}

			return await next();

		}
	}
}
