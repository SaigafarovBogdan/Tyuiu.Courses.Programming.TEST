using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class TaskAnswerScoreEntity(int id) : Entity(id)
	{
		public int TaskAnswerId { get; set; }
		public double Score { get; set; }
		public double? ModeratorScore { get; set; }
		public string? ModeratorId { get; set; }

		public virtual TaskAnswerEntity TaskAnswer { get; set; } = default!;
	}
}
