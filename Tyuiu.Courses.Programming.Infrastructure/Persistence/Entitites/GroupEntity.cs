using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class GroupEntity: Entity
	{
		public string Name { get; set; } = default!;

		public virtual List<UserEntity> Students { get; set; } = [];
	}
}
