namespace Tyuiu.Courses.Programming.Api.Extensions.WebApplicationExtensions
{
	public static class EnvironmentConfiguration
	{
		public static async Task<WebApplication> ConfigureEnvironment(this WebApplication app, IServiceProvider serviceProvider)
		{
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();

				await app.SeedDatabaseAsync(serviceProvider);
			}
			else
			{
				app.UseExceptionHandler("/Error/StatusCode/500");
				app.UseHsts();
			}

			return app;
		}
	}
}
