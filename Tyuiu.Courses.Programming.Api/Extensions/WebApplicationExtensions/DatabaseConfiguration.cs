using Microsoft.EntityFrameworkCore;
using Tyuiu.Courses.Programming.Infrastructure.Persistence;

namespace Tyuiu.Courses.Programming.Api.Extensions.WebApplicationExtensions
{
	public static class DatabaseConfiguration
	{
		public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
		{
			builder.AddNpgsqlDbContext<DatabaseContext>("orderdb", configureDbContextOptions: options =>
			{
				options.UseNpgsql(npgsqlOptions =>
				{
					npgsqlOptions.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName);
					npgsqlOptions.EnableRetryOnFailure(3);
				});

				if (builder.Environment.IsDevelopment())
				{
					options.EnableSensitiveDataLogging();
					options.EnableDetailedErrors();

					builder.Services.AddDatabaseDeveloperPageExceptionFilter();
				}
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

		public static async Task SeedDatabaseAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			//var seed = scope.ServiceProvider.GetRequiredService<DbSeeder>();
			//await seed.InitializeAsync();
		}
	}
}
