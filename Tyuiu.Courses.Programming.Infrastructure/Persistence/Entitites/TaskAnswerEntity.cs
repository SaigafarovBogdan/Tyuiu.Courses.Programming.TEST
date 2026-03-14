using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class TaskAnswerEntity(int id) : Entity(id)
	{
		public string UserId { get; set; } = default!;
		public int CourseSprintId { get; set; }
		public int CourseThemeId { get; set; }
		public int TaskId { get; set; }
		public string GitRepoUrl { get; set; } = default!;
		public int CountTaskChanges { get; set; }
		public int CountAnswerTries { get; set; }
		public DateTime PassDate { get; set; }
		public string? Status { get; set; }

		public virtual CourseThemeEntity? CourseTheme { get; set; }
		public virtual ThemeTaskEntity Task { get; set; } = default!;
		public virtual UserEntity User { get; set; } = default!;
		public virtual TaskAnswerScoreEntity Scores { get; set; } = default!;
		public virtual List<AnswerCommentEntity> Comments { get; set; } = [];
	}
}
