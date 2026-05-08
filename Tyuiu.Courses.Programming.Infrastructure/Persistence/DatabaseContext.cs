using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence
{
	public class DatabaseContext : IdentityDbContext<UserEntity>
	{
		private IDbContextTransaction? _currentTransaction;

		public DatabaseContext(DbContextOptions<DatabaseContext> options)
			: base(options) { }

		public bool HasActiveTransaction => _currentTransaction != null;
		public IDbContextTransaction? CurrentTransaction => _currentTransaction;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
		{
			if (_currentTransaction != null)
				throw new InvalidOperationException("A transaction is already in progress");

			_currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
			return _currentTransaction;
		}

		public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(transaction);

			if (transaction != _currentTransaction)
				throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

			try
			{
				await SaveChangesAsync(cancellationToken);
				await transaction.CommitAsync(cancellationToken);
			}
			catch
			{
				await RollbackTransactionAsync();
				throw;
			}
			finally
			{
				if (HasActiveTransaction)
				{
					_currentTransaction!.Dispose();
					_currentTransaction = null;
				}
			}
		}

		public async Task RollbackTransactionAsync()
		{
			try
			{
				if (_currentTransaction != null)
				{
					await _currentTransaction.RollbackAsync();
				}
			}
			finally
			{
				if (HasActiveTransaction)
				{
					_currentTransaction?.Dispose();
					_currentTransaction = null;
				}
			}
		}
	}
}
