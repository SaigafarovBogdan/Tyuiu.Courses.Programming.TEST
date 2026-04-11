using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class ThemeTaskEntity: Entity
	{
		public int Variant { get; set; }
		public string Description { get; set; } = default!;
		public int ThemeId { get; set; }

		public virtual ThemeEntity? Theme { get; set; }
	}
}
