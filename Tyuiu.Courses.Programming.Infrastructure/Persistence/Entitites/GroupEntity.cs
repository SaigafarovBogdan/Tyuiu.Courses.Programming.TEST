using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class GroupEntity(int id) : Entity(id)
	{
		public string Name { get; set; } = default!;

		public virtual List<UserEntity> Students { get; set; } = [];
	}
}
