using Tyuiu.Courses.Programming.Core.Shared;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;

namespace Tyuiu.Courses.Programming.Infrastructure.Providers.Identity
{
	public interface IUserService
	{
		string GetCurrentUserId();
		string GetCurrentUserRole();
		string GetCurrentUserGitName();
		bool HasUser(string? id = null, string? username = null, string? email = null);
		Task<UserEntity?> GetById(string id);
		Task<UserEntity?> GetByUsername(string username);
		Task<IList<UserEntity>> GetAllByRole(string role);
		Task CreateOrUpdateUser(UserEntity user, string password, string? role);
		Task<bool> DeleteUser(string id);
		Task<Result> ChangePassword(string userId, string currentPassword, string newPassword, string confirmPassword);
	}
}
