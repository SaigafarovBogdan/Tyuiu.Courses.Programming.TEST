using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tyuiu.Courses.Programming.Core.Shared;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Repositories
{
	public class Repository<T>(DbSet<T> dbSet) : IRepository<T> where T : class, IEntity
	{
		protected readonly DbSet<T> _dbSet = dbSet;

		public virtual async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			return await _dbSet.FindAsync(new[] { id }, cancellationToken);
		}

		public virtual async Task<T?> GetByIdAsync(object id,
			Func<IQueryable<T>, IQueryable<T>>? includeFunc = null,
			CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = _dbSet;

			if (includeFunc != null)
				query = includeFunc(query);

			return await query.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
		}

		public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate,
			CancellationToken cancellationToken = default)
		{
			return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
		}

		public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate,
			Func<IQueryable<T>, IQueryable<T>>? includeFunc = null,
			CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = _dbSet.Where(predicate);

			if (includeFunc != null)
				query = includeFunc(query);

			return await query.ToListAsync(cancellationToken);
		}

		public virtual void Add(IEnumerable<T> entities)
		{
			_dbSet.AddRange(entities);
		}

		public virtual void Update(IEnumerable<T> entities)
		{
			_dbSet.UpdateRange(entities);
		}

		public virtual void Remove(IEnumerable<T> entities)
		{
			_dbSet.RemoveRange(entities);
		}
	}
}
