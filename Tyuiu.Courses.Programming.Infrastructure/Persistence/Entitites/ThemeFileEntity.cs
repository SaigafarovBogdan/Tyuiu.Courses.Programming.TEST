using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class ThemeFileEntity: Entity
	{
		public int ThemeId { get; set; }
		public int FileId { get; set; }

		public virtual ThemeEntity? Theme { get; set; } = null!;
		public virtual FileDataEntity? FileData { get; set; } = null!;
	}
}
