using Tyuiu.Courses.Programming.Api.Extensions.WebApplicationExtensions;
using Tyuiu.Courses.Programming.Infrastructure.Persistence;

namespace Tyuiu.Courses.Programming.Api
{
	public class Program
	{
		public async static Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.AddServiceDefaults();

			builder.ConfigureLogging()
				   .ConfigureDatabase();

			builder.Services.AddControllers();

			var app = builder.Build();

			var serviceProvider = app.Services.CreateScope().ServiceProvider;

			await app.ApplyMigrationsAsync();

			UpdateDbData updateDbData = new(serviceProvider);
			await updateDbData.InitializeAsync();

			app.ConfigureEnvironment()
			   .ConfigureLocalization();

			app.UseHttpsRedirection();
			app.UseStaticFiles(new StaticFileOptions
			{
				OnPrepareResponse = ctx =>
					ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=604800")
			});

			app.UseRouting();

			app.MapDefaultEndpoints();
			app.MapControllers();

			app.Run();
		}
	}
}
