using System.Globalization;

namespace Tyuiu.Courses.Programming.Api.Extensions.WebApplicationExtensions
{
	public static class LocalizationConfiguration
	{
		public static WebApplication ConfigureLocalization(this WebApplication app)
		{
			var culture = new CultureInfo("ru-RU");
			CultureInfo.DefaultThreadCurrentCulture = culture;
			CultureInfo.DefaultThreadCurrentUICulture = culture;

			return app;
		}
	}
}
