using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class FileDataEntity(int id) : Entity(id)
	{
		public string OriginalName { get; set; } = default!;
		public string Guid { get; set; } = default!;
		public string Extension { get; set; } = default!;
	}
}
