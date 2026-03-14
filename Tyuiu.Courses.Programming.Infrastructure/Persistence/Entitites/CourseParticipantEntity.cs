using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class CourseParticipantEntity(int id) : Entity(id)
	{
		public string UserId { get; set; } = default!;
		public bool isModerator { get; set; }
		public int CourseId { get; set; }

		public virtual UserEntity User { get; set; } = default!;
		public virtual CourseEntity? Course { get; set; }
	}
}