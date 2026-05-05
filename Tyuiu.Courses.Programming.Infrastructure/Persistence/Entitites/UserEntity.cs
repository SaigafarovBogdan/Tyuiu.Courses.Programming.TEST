using Microsoft.AspNetCore.Identity;
using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites
{
	public class UserEntity() : IdentityUser, IEntity
	{
		public string Name { get; set; } = default!;
		public string Surname { get; set; } = default!;
		public string? Patronymic { get; set; }
		public int? GroupId { get; set; }

		object IEntity.Id => Id;
		public string FullName => string.Join(" ", [Surname, Name, Patronymic]);
		public string InitialedName => $"{Surname} {Name?.First()}.{(!string.IsNullOrEmpty(Patronymic) ? Patronymic.First() + "." : "")}";

		public virtual GroupEntity? Group { get; set; }
	}
}