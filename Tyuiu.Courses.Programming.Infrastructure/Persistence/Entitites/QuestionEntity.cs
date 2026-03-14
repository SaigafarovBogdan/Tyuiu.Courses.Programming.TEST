using Tyuiu.Courses.Programming.Core.Enums;
using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class QuestionEntity(int id) : Entity(id)
	{
		public int TestId { get; set; }
		public string Title { get; set; } = default!;
		public string Description { get; set; } = default!;
		public QuestionType Type { get; set; }
		public bool IgnoreCase { get; set; }

		public virtual List<QuestionAnswerEntity> Answers { get; set; } = [];
		public virtual ThemeTestEntity? ThemeTest { get; set; }
	}
}
