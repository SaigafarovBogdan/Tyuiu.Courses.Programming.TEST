using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class ThemeTestEntity(int id) : Entity(id)
	{
		public int ThemeId { get; set; }
		public int QuestionsCount { get; set; }
		public int PassingScore { get; set; }
		public int Duration { get; set; }
		public bool SavedByUser { get; set; }

		public virtual ThemeEntity? Theme { get; set; }
		public virtual List<QuestionEntity> Questions { get; set; } = [];
	}
}
