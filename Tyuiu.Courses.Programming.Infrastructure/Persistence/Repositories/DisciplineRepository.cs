using Microsoft.EntityFrameworkCore;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Repositories
{
	public class DisciplineRepository : Repository<DisciplineEntity>, IDisciplineRepository
	{
		public DisciplineRepository(DbSet<DisciplineEntity> context) : base(context) { }

		public Task<(ICollection<DisciplineEntity> Disciplines, int DisciplinesCount)> GetWithPagination(int page = 1, int pageSize = 10, string? searchTerm = null, string? userId = null, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}
}
