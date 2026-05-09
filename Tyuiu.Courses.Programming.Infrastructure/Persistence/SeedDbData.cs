using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Tyuiu.Courses.Programming.Core.Constants;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;
using Tyuiu.Courses.Programming.Infrastructure.Providers.Identity;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence
{
	public class SeedDbData
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly DatabaseContext _context;

		// true: seed database data (ON)
		// false: skip seed database data (OFF)
		private readonly bool seedData = true;

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
			await SeedDisciplines();

			await _context.SaveChangesAsync();
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

		private async Task<List<DisciplineEntity>> SeedDisciplines()
		{
			int disciplinesCount = 10;
			int sprintsCount = 8;
			int themesCount = 8;
			int tasksCount = 1;

			var disciplines = new List<DisciplineEntity>();
			var adminId = _context?.Users?.FirstOrDefault(u => u.UserName == "admin@example.com")!.Id;

			for (int d = 0; d < disciplinesCount; d++)
			{
				var disciplineName = $"Дисциплина {d + 1}";

				var existingDiscipline = _context?.Disciplines?.FirstOrDefault(i => i.Name == disciplineName);
				if (existingDiscipline != null)
				{
					disciplines.Add(existingDiscipline);
					continue;
				}

				var discipline = new DisciplineEntity()
				{
					Name = disciplineName,
					AuthorId = adminId
				};

				_context?.Disciplines?.Add(discipline);
				disciplines.Add(discipline);

				for (int s = 0; s < sprintsCount; s++)
				{
					var newSprint = new SprintEntity()
					{
						DisciplineId = discipline.Id,
						Name = $"Спринт {s}",
						IndexNumber = s,
					};

					await _context.AddAsync(newSprint);
					if (s != 1)
					{
						await GenerateThemes(newSprint, themesCount, tasksCount);
					}
					else
					{
						var themesNames = new List<string>() {
							"0 - Некорректное название таска. https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1",
							"1 - Библиотека не скомпилировалась. https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1",
							"2 - Ошибка интерфейса. https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1",
							"3 - Не сошлись ответы. https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1",
							"4 - Все хорошо. https://github.com/clipboard1/Tyuiu.SimonSRTests.Sprint1",
						};
						await GenerateThemes(newSprint, themesCount, tasksCount, themesNames);
					}
				}
			}

			return disciplines;
		}

		private async Task GenerateThemes(SprintEntity newSprint, int themesCount, int tasksCount, List<string>? themesNames = null)
		{
			for (int t = 0; t < (themesNames?.Count ?? themesCount); t++)
			{
				var newTheme = new ThemeEntity()
				{
					SprintId = newSprint.Id,
					Name = themesNames?.ElementAtOrDefault(t) ?? $"Спринт {newSprint.IndexNumber}. Тема {t}",
					IndexNumber = t,
					Introduction = $"Вступление {t}",
					Theory = $"Теория {t}",
					AutoCheckingTasks = newSprint.IndexNumber != 0
				};

				await _context.AddAsync(newTheme);

				if (themesNames == null)
				{
					for (int tt = 0; tt < tasksCount; tt++)
					{
						var newTask = new ThemeTaskEntity()
						{
							Variant = tt,
							Description = $"Для проверки таска с другим вариантом: измени вариант в настройках этого таска",
							ThemeId = newTheme.Id,
						};
						await _context.AddAsync(newTask);
					}
				}
				else
				{
					var newTask = new ThemeTaskEntity()
					{
						Variant = 10,
						Description = themesNames[t],
						ThemeId = newTheme.Id,
					};
					await _context.AddAsync(newTask);
				}
			}
		}
	}
}
