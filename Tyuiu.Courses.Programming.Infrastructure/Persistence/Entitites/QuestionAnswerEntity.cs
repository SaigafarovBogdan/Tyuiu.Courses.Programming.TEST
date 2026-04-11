using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class QuestionAnswerEntity: Entity
	{
		public string Text { get; set; } = default!;
		public bool IsCorrect { get; set; }
		public int QuestionId { get; set; }

		public virtual QuestionEntity? Question { get; set; }
	}
}
