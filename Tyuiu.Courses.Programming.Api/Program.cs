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
				   .ConfigureDatabase()
				   .ConfigureIdentity()
				   .ConfigureServices();

			var app = builder.Build();

			var serviceProvider = app.Services.CreateScope().ServiceProvider;

			await app.ApplyMigrationsAsync();

			UpdateDbData updateDbData = new(serviceProvider);
			await updateDbData.InitializeAsync();

			app.UseStatusCodePagesWithReExecute("/Error/StatusCode/{0}");

			(await app.ConfigureEnvironment(serviceProvider))
			    .ConfigureLocalization();

			app.UseHttpsRedirection();
			app.UseStaticFiles(new StaticFileOptions
			{
				OnPrepareResponse = ctx =>
					ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=604800")
			});

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapDefaultEndpoints();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Courses}/{action=Learn}");
			app.MapControllerRoute(
				name: "error",
				pattern: "{controller=Error}/{action=StatusCode}/{code}");
			app.MapRazorPages();

			app.Run();
		}
	}
}
