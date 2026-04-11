namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions
{
	public interface IUnitOfWork
	{
		IDisciplinesRepository Disciplines { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
