using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions
{
	public interface IUnitOfWork
	{
		IRepository<DisciplineEntity> Disciplines { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
