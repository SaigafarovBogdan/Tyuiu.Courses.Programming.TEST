using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class CheckpointEntity: Entity
	{
		public int CourseSprintId { get; set; }
		public string Name { get; set; } = default!;
		public string Location { get; set; } = default!;
		public string? Description { get; set; }
		public DateTime Date { get; set; }

		public virtual CourseSprintEntity? CourseSprint { get; set; }
	}
}
