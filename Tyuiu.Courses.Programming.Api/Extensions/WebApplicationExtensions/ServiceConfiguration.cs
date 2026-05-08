using Tyuiu.Courses.Programming.Application;
using Tyuiu.Courses.Programming.Infrastructure.Persistence;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Abstractions;
using Tyuiu.Courses.Programming.Infrastructure.Providers.Identity;

namespace Tyuiu.Courses.Programming.Api.Extensions.WebApplicationExtensions
{
	public static class ServiceConfiguration
	{
		public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddMemoryCache();
			builder.Services.AddControllersWithViews();

			if (!builder.Environment.IsDevelopment())
			{
				builder.WebHost.ConfigureKestrel(options => options.ListenAnyIP(80));

				builder.Services.AddDatabaseDeveloperPageExceptionFilter();
			}

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IUserService, UserService>();

			builder.Services.AddApplication();

			return builder;
		}
	}
}
