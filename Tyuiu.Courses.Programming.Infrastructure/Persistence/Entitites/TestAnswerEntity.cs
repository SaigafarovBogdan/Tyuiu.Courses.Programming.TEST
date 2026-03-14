using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class TestAnswerEntity(int id) : Entity(id)
	{
		public int TestResultId { get; set; }
		public int QuestionId { get; set; }
		public List<string> Answer { get; set; } = [];
		public bool IsCorrect { get; set; }

		public virtual TestResultEntity TestResult { get; set; } = default!;
	}
}
