using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class DisciplineEntity(int id) : Entity(id)
	{
		public string Name { get; set; } = default!;
		public string? AuthorId { get; set; }

		public virtual UserEntity? Author { get; set; }
		public virtual List<SprintEntity>? Sprints { get; set; }
		public virtual List<CourseEntity>? Courses { get; set; }
	}
}
