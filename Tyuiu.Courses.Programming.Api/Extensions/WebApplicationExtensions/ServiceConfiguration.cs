using Tyuiu.Courses.Programming.Application;

namespace Tyuiu.Courses.Programming.Api.Extensions.WebApplicationExtensions
{
	public static class ServiceConfiguration
	{
		public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
		{
			if (!builder.Environment.IsDevelopment())
			{
				builder.WebHost.ConfigureKestrel(options => options.ListenAnyIP(80));
			}

			builder.Services.AddApplication();

			return builder;
		}
	}
}
