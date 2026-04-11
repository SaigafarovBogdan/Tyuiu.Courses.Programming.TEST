using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class SprintEntity: Entity
	{
		public string Name { get; set; } = default!;
		public int DisciplineId { get; set; }
		public int? IndexNumber { get; set; }

		public virtual DisciplineEntity? Discipline { get; set; }
		public virtual List<ThemeEntity> Themes { get; set; } = [];
	}
}
