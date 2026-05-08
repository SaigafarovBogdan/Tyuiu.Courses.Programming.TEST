using Microsoft.EntityFrameworkCore;
using Tyuiu.Courses.Programming.Infrastructure.Persistence;

namespace Tyuiu.Courses.Programming.Api.Extensions.WebApplicationExtensions
{
	public static class DatabaseConfiguration
	{
		public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

			builder.AddNpgsqlDbContext<DatabaseContext>("aspnetappdb",
				configureDbContextOptions: options =>
				{
					if (builder.Environment.IsDevelopment())
					{
						options.EnableSensitiveDataLogging();
						options.EnableDetailedErrors();
					}
					options.UseNpgsql(b => b.MigrationsAssembly("Tyuiu.Courses.Programming.Infrastructure"));
				});

			return builder;
		}

		public static async Task ApplyMigrationsAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			using var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

			if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
			{
				await dbContext.Database.MigrateAsync();
			}
		}
		public static async Task SeedDatabaseAsync(this WebApplication app, IServiceProvider serviceProvider)
		{
			SeedDbData seedDbData = new(serviceProvider);
			await seedDbData.InitializeAsync();
		}
	}
}
