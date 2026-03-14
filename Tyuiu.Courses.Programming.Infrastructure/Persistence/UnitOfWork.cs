using Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Repositories;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DatabaseContext _context;
		private IRepository<DisciplineEntity>? _disciplines;

		public UnitOfWork(DatabaseContext context)
		{
			_context = context;
		}

		public IRepository<DisciplineEntity> Disciplines =>
			_disciplines ??= new Repository<DisciplineEntity>(_context.Set<DisciplineEntity>());

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await _context.SaveChangesAsync(cancellationToken);
		}
	}
}

