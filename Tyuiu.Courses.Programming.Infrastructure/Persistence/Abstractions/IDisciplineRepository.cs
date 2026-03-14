using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions
{
	public interface IDisciplineRepository: IRepository<DisciplineEntity>
	{
		Task<(ICollection<DisciplineEntity> Disciplines, int DisciplinesCount)> GetWithPagination(
			int page = 1,
			int pageSize = 10,
			string? searchTerm = null,
			string? userId = null,
			CancellationToken cancellationToken = default);
	}
}
