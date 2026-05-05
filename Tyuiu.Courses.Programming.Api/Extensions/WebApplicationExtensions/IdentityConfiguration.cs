using Microsoft.AspNetCore.Identity;
using Tyuiu.Courses.Programming.Infrastructure.Persistence;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;

namespace Tyuiu.Courses.Programming.Api.Extensions.WebApplicationExtensions
{
	public static class IdentityConfiguration
	{
		public static WebApplicationBuilder ConfigureIdentity(this WebApplicationBuilder builder)
		{
			builder.Services.AddDefaultIdentity<UserEntity>(options =>
			{
				options.SignIn.RequireConfirmedAccount = true;
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 7;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.User.RequireUniqueEmail = true;
			})
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<DatabaseContext>()
			.AddDefaultTokenProviders();

			builder.Services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.Name = ".AspNetCore.Identity.Application";
				options.ExpireTimeSpan = TimeSpan.FromDays(30);
				options.SlidingExpiration = true;
			});

			builder.Services.Configure<SecurityStampValidatorOptions>(o =>
					   o.ValidationInterval = TimeSpan.FromDays(30));

			return builder;
		}
	}
}
