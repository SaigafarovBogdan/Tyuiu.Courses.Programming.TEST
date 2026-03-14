using System.Linq.Expressions;
using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions
{
	public interface IRepository<T> where T : IEntity
	{
		Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
		Task<T?> GetByIdAsync(object id, Func<IQueryable<T>, IQueryable<T>>? includeFunc = null,
			CancellationToken cancellationToken = default);

		Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate,
			CancellationToken cancellationToken = default);
		Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate,
			Func<IQueryable<T>, IQueryable<T>>? includeFunc = null,
			CancellationToken cancellationToken = default);

		void Add(IEnumerable<T> entities);
		void Update(IEnumerable<T> entities);
		void Remove(IEnumerable<T> entities);
	}
}
