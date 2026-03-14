using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class ThemeEntity(int id) : Entity(id)
	{
		public string Name { get; set; } = default!;
		public int SprintId { get; set; }
		public int IndexNumber { get; set; }
		public string? Introduction { get; set; }
		public string? Theory { get; set; }
		public string? VideoURL { get; set; }
		public bool AutoCheckingTasks { get; set; }

		public virtual SprintEntity? Sprint { get; set; }
		public virtual List<ThemeTaskEntity> Tasks { get; set; } = [];
		public virtual List<ThemeFileEntity> Files { get; set; } = [];
		public virtual ThemeTestEntity? Test { get; set; }
	}
}

