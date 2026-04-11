using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class CourseSprintEntity: Entity
	{
		public int CourseId { get; set; }
		public int SprintId { get; set; }
		public int? IndexNumber { get; set; }
		public bool IsHidden { get; set; }

		public virtual CourseEntity Course { get; set; } = default!;
		public virtual SprintEntity Sprint { get; set; } = default!;
		public virtual List<CourseThemeEntity> Themes { get; set; } = [];
		public virtual CheckpointEntity Checkpoint { get; set; } = default!;
	}
}
