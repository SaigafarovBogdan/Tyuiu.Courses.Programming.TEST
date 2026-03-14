using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class CourseEntity(int id) : Entity(id)
	{
		public string Name { get; set; } = default!;
		public int? DisciplineId { get; set; }
		public int? GroupId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public virtual DisciplineEntity? Discipline { get; set; }
		public virtual GroupEntity? Group { get; set; }
		public virtual List<CourseSprintEntity> CourseSprints { get; set; } = [];
		public virtual List<CourseParticipantEntity> Participants { get; } = [];
	}

}