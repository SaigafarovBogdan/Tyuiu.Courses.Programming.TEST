using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Tyuiu.Courses.Programming.Core.Extensions;
using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Application.Behaviors
{
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TResponse : Result
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;
		private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

		public ValidationBehavior(
			IEnumerable<IValidator<TRequest>> validators,
			ILogger<ValidationBehavior<TRequest, TResponse>> logger)
		{
			_validators = validators;
			_logger = logger;
		}

		public async Task<TResponse> Handle(
			TRequest request,
			RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken)
		{
			if (!_validators.Any())
				return await next(cancellationToken);

			var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request, cancellationToken)));

			var failures = validationResults
				.SelectMany(result => result.Errors)
				.Where(error => error != null)
				.ToList();

			if (failures.Count != 0)
			{
				_logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", request.GetGenericTypeName(), request, failures);

				var errors = failures.Select(f =>
					Error.Validation(f.PropertyName, f.ErrorMessage, f.ErrorCode)).ToList();

				return CreateFailureResponse(Result.Invalid(errors));
			}

			return await next(cancellationToken);
		}

		private static TResponse CreateFailureResponse(Result result)
		{
			if (typeof(TResponse).IsGenericType)
			{
				var genericArg = typeof(TResponse).GetGenericArguments()[0];
				var failureMethod = typeof(Result<>)
					.MakeGenericType(genericArg)
					.GetMethod("Invalid", new[] { typeof(List<Error>) });
				return (TResponse)failureMethod!.Invoke(null, new object[] { result.Errors.ToList() })!;
			}
			else
			{
				return (TResponse)(object)result;
			}
		}
	}
}
