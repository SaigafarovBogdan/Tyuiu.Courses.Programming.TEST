using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tyuiu.Courses.Programming.Application.Abstractions;
using Tyuiu.Courses.Programming.Core.Shared;
using Tyuiu.Courses.Programming.Infrastructure.Persistence;

namespace Tyuiu.Courses.Programming.Application.Behaviors
{
	public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TResponse : Result
	{
		private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
		private readonly DatabaseContext _context;

		public TransactionBehavior(DatabaseContext context,
			ILogger<TransactionBehavior<TRequest, TResponse>> logger)
		{
			_context = context ?? throw new ArgumentException(nameof(DatabaseContext));
			_logger = logger ?? throw new ArgumentException(nameof(ILogger));
		}

		public async Task<TResponse> Handle(
			TRequest request,
			RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken)
		{
			if (request is IQuery || _context.HasActiveTransaction)
			{
				return await next(cancellationToken);
			}
			var requestName = typeof(TRequest).Name;

			try
			{
				_logger.LogInformation("Beginning transaction for {RequestType}", requestName);

				var strategy = _context.Database.CreateExecutionStrategy();

				TResponse? response = null;

				await strategy.ExecuteAsync(async () =>
				{
					await using var transaction = await _context.BeginTransactionAsync(cancellationToken);

					using (_logger.BeginScope(new Dictionary<string, object>
					{
						["TransactionId"] = transaction.TransactionId,
						["CommandName"] = requestName
					}))
					{
						response = await next(cancellationToken);

						if (response.IsSuccess)
						{
							await _context.CommitTransactionAsync(transaction, cancellationToken);
						}
						else
						{
							_logger.LogDebug("Rolling back transaction for {RequestType} due to failure",
								requestName);
							await _context.RollbackTransactionAsync();
						}
					}
				});

				return response!;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in transaction for {RequestType}, rolling back", requestName);
				throw;
			}
		}
	}
}
