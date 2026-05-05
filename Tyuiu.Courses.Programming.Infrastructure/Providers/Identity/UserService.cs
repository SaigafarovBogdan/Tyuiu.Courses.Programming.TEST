using System.Security.Claims;
using Tyuiu.Courses.Programming.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;
using Microsoft.AspNetCore.Http;
using Tyuiu.Courses.Programming.Core.Shared.Helpers;
using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Infrastructure.Providers.Identity
{
	public class UserService : IUserService
	{
		private readonly DatabaseContext _context;
		private readonly UserManager<UserEntity> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserService(
			DatabaseContext context,
			UserManager<UserEntity> userManager,
			IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}

		private bool IsAuthenticated() =>
			_httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true;

		public string GetCurrentUserId() =>
			IsAuthenticated()
				? _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value
				: string.Empty;

		public string GetCurrentUserRole() =>
			IsAuthenticated()
				? _httpContextAccessor.HttpContext!.User.FindFirst("UserRoles")?.Value ?? string.Empty
				: string.Empty;

		public string GetCurrentUserGitName()
		{
			if (!IsAuthenticated()) return string.Empty;

			var user = _httpContextAccessor.HttpContext!.User;
			string surname = user.FindFirstValue("Surname") ?? "";
			char? nameChar = GetFirstChar(user.FindFirstValue("Name"));
			char? patronymicChar = GetFirstChar(user.FindFirstValue("Patronymic"));
			return Transliterator.CyrillicToLatin($"{surname}{nameChar}{patronymicChar}");
		}

		public async Task<UserEntity?> GetCurrentUser()
		{
			string id = GetCurrentUserId();
			return string.IsNullOrEmpty(id) ? null : await GetById(id);
		}

		public async Task<UserEntity?> GetById(string id) =>
			await _userManager.FindByIdAsync(id);

		public async Task<UserEntity?> GetByUsername(string username) =>
			await _userManager.FindByNameAsync(username);

		public async Task<IList<UserEntity>> GetAllByRole(string role) =>
			await _userManager.GetUsersInRoleAsync(role);


		private async Task<bool> AddUser(UserEntity user, string password, string? role = null)
		{
			if (string.IsNullOrEmpty(user.UserName))
				user.UserName = GenerateUserName(user);

			var result = await _userManager.CreateAsync(user, password);
			if (result.Succeeded && !string.IsNullOrEmpty(role))
				await _userManager.AddToRoleAsync(user, role);

			return result.Succeeded;
		}

		public async Task CreateOrUpdateUser(UserEntity user, string password, string? role = null)
		{
			if (string.IsNullOrEmpty(user.UserName))
				throw new ArgumentNullException(nameof(user.UserName), "UserName не может быть null");

			var existing = await GetByUsername(user.UserName);
			if (existing == null)
			{
				await AddUser(user, password, role);
			}
			else
			{
				existing.Name = user.Name;
				existing.Surname = user.Surname;
				existing.Patronymic = user.Patronymic;
				existing.GroupId = user.GroupId;
				existing.Email = user.Email;
				existing.EmailConfirmed = user.EmailConfirmed;
				await UpdateUser(existing, role);
			}
		}

		private async Task UpdateUser(UserEntity user, string? role = null)
		{
			await _userManager.UpdateAsync(user);
			if (role != null)
			{
				var currentRoles = await _userManager.GetRolesAsync(user);
				await _userManager.RemoveFromRolesAsync(user, currentRoles);
				await _userManager.AddToRoleAsync(user, role);
			}
		}

		public async Task<Result> ChangePassword(string userId, string currentPassword, string newPassword, string confirmPassword)
		{
			var user = await GetById(userId);
			if (user == null) return Result<int>.NotFound(Error.NotFound("Пользователь не найден"));

			if (string.IsNullOrEmpty(newPassword) || newPassword != confirmPassword)
			{
				var error = new Error(
					code: "User.PasswordsDontMatch",
					message: "Пароли не совпадают",
					property: nameof(newPassword)
				);
				return Result.Invalid(error);
			}

			var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
			if (!result.Succeeded)
			{
				var passwordMismatchError = result.Errors.Any(error => error.Code == "PasswordMismatch");
				if (passwordMismatchError)
				{
					var error = new Error(
						code: "User.InvalidCurrentPassword",
						message: "Неверный текущий пароль",
						property: nameof(currentPassword)
					);
					return Result.Invalid(error);
				}

				return Result.Invalid(result.Errors.Select(e => new Error(
					code: $"User.{e.Code}",
					message: e.Description,
					property: nameof(newPassword)
				)).ToList());
			}

			return Result.Success();
		}

		private static string GenerateUserName(UserEntity user)
		{
			string cyrillicUsername = user.Surname.ToLowerInvariant()
									+ user.Name[..1].ToLowerInvariant();
			if (!string.IsNullOrEmpty(user.Patronymic))
				cyrillicUsername += user.Patronymic[..1].ToLowerInvariant();

			return Transliterator.CyrillicToLatin(cyrillicUsername);
		}

		public bool HasUser(string? id = null, string? username = null, string? email = null)
		{
			if (id != null)
				return _context.Users.Any(u => u.Id == id);
			if (username != null)
				return _context.Users.Any(u => u.UserName == username);
			if (email != null)
				return _context.Users.Any(u => u.Email == email);
			return false;
		}

		public async Task<bool> DeleteUser(string id)
		{
			var user = await GetById(id);
			if (user == null) return false;

			await _userManager.DeleteAsync(user);
			return true;
		}

		private static char? GetFirstChar(string? str) =>
			string.IsNullOrEmpty(str) ? null : str[0];
	}
}
