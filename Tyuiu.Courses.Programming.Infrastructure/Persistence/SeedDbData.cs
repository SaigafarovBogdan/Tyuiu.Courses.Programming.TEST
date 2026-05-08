using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Tyuiu.Courses.Programming.Core.Constants;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;
using Tyuiu.Courses.Programming.Infrastructure.Providers.Identity;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence
{
	public class SeedDbData
	{
		private IServiceProvider _serviceProvider { get; }
		private DatabaseContext _context { get; }

		// true: seed database data (ON)
		// false: skip seed database data (OFF)
		private bool seedData = true;

		public SeedDbData(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
			_context = serviceProvider.GetRequiredService<DatabaseContext>();
		}

		public async Task InitializeAsync()
		{
			if (!seedData) return;

			await SeedRoles();
			await SeedUsers();
		}

		private async Task SeedRoles()
		{
			var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			var roles = new[] { Roles.Admin, Roles.Teacher, Roles.Student };

			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}

		private async Task<List<UserEntity>> SeedUsers()
		{
			var userService = _serviceProvider.GetRequiredService<IUserService>();

			var adminUser = new UserEntity
			{
				UserName = "admin@example.com",
				Email = "admin@example.com",
				Name = "AdminFirstName",
				Surname = "AdminLastName2",
				Patronymic = "AdminThirdName1",
				EmailConfirmed = true,
			};

			var teacherUser = new UserEntity
			{
				UserName = "teacher@example.com",
				Email = "teacher@example.com",
				Name = "TeacherFirstName",
				Surname = "TeacherLastName",
				Patronymic = "TeacherThirdName",
				EmailConfirmed = true,
			};

			var studentUser = new UserEntity
			{
				UserName = "student@example.com",
				Email = "student@example.com",
				Name = "StudentFirstName",
				Surname = "StudentLastName",
				Patronymic = "StudentThirdName",
				EmailConfirmed = true,
			};

			await userService.CreateOrUpdateUser(adminUser, "Admin123!", Roles.Admin);
			await userService.CreateOrUpdateUser(teacherUser, "Teacher123!", Roles.Teacher);
			await userService.CreateOrUpdateUser(studentUser, "Student123!", Roles.Student);

			return [adminUser, teacherUser, studentUser];
		}
	}
}
