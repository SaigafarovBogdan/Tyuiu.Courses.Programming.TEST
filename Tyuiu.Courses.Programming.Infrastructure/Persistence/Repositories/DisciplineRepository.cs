using Microsoft.EntityFrameworkCore;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Repositories
{
	public class DisciplineRepository : Repository<DisciplineEntity>, IDisciplinesRepository
	{
		public DisciplineRepository(DbSet<DisciplineEntity> dbSet) : base(dbSet) { }

		public async Task<(ICollection<DisciplineEntity> Disciplines, int DisciplinesCount)> GetWithPagination(
			int page = 1, int pageSize = 10,
			string? searchTerm = null, string? userId = null,
			CancellationToken cancellationToken = default)
		{
			var normalizedSearchTerm = string.IsNullOrWhiteSpace(searchTerm) ? null : searchTerm.ToLower();

			IQueryable<DisciplineEntity> query = _dbSet.AsNoTracking();

			if (normalizedSearchTerm != null)
			{
				query = query.Where(d => d.Name.ToLower().Contains(normalizedSearchTerm));
			}

			var totalCount = await query.CountAsync(cancellationToken);

			query = query
				.Include(d => d.Author)
				.Include(d => d.Courses)
				.OrderByDescending(d => d.AuthorId == userId);

			var items = await query
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync(cancellationToken);

			return (items, totalCount);
		}

		public async Task<bool> IsUniqueName(string name, int? excludeId = null, CancellationToken cancellationToken = default)
		{
			var query = _dbSet.Where(d => d.Name == name);
			if (excludeId.HasValue)
				query = query.Where(d => d.Id != excludeId.Value);

			return !await query.AnyAsync(cancellationToken);
		}

		public async Task<DisciplineEntity?> GetByIdFullAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _dbSet
				.AsNoTracking()
				.Include(d => d.Sprints!)
					.ThenInclude(s => s.Themes!)
						.ThenInclude(t => t.Files)
				.Include(d => d.Sprints!)
					.ThenInclude(s => s.Themes!)
						.ThenInclude(t => t.Tasks)
				.Include(d => d.Sprints!)
					.ThenInclude(s => s.Themes!)
						.ThenInclude(t => t.Test!)
							.ThenInclude(test => test.Questions!)
								.ThenInclude(q => q.Answers)
				.AsSplitQuery()
				.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
		}
	}
}
