using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;

namespace Tyuiu.Courses.Programming.Application.Abstractions
{
	public interface IUserService
	{
		public string GetCurrentUserId();
		public string GetCurrentUserRole();
		public string GetCurrentUserGitName();
		public Task<UserEntity> GetById(string id);
		public Task<IList<UserEntity>> GetAllByRole(string role);
		public Task CreateOrUpdateUser(UserEntity user, string password, string? role);
		public Task AssignGroup(string userId, int groupId);
		public Task RemoveFromGroup(string userId);
	}
}
