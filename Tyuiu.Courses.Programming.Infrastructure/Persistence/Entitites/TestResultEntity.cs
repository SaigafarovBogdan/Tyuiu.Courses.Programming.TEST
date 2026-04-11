using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class TestResultEntity: Entity
	{
		public int CourseThemeId { get; set; }
		public string UserId { get; set; } = default!;
		public int Score { get; set; }
		public bool IsPassed { get; set; }

		public virtual CourseThemeEntity CourseTheme { get; set; } = default!;
		public virtual List<TestAnswerEntity> TestAnswers { get; set; } = [];
	}
}
