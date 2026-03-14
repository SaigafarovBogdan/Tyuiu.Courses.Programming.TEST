namespace Tyuiu.Courses.Programming.Api.Extensions.WebApplicationExtensions
{
	public static class EnvironmentConfiguration
	{
		public static WebApplication ConfigureEnvironment(this WebApplication app)
		{
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
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
