using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class CourseThemeEntity(int id) : Entity(id)
	{
		public int CourseSprintId { get; set; }
		public int ThemeId { get; set; }
		public int? IndexNumber { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool IsHidden { get; set; }

		public virtual CourseSprintEntity CourseSprint { get; set; } = default!;
		public virtual ThemeEntity Theme { get; set; } = default!;
	}
}