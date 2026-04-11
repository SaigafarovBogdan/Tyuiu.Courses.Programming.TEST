using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class AnswerCommentEntity: Entity
	{
		public int TaskAnswerId { get; set; }
		public string TeacherId { get; set; } = default!;
		public DateTime TimeStamp { get; set; }
		public string Text { get; set; } = default!;

		public virtual TaskAnswerEntity? TaskAnswer { get; set; }
		public virtual UserEntity? Teacher { get; set; }
	}
}
