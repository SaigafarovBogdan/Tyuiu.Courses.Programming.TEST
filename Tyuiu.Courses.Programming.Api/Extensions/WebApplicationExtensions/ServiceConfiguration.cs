using FluentValidation;
using MediatR;
using Tyuiu.Courses.Programming.Application.Behaviors;

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

			builder.Services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(typeof(ServiceConfiguration).Assembly);

				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
			});

			builder.Services.AddValidatorsFromAssembly(typeof(ServiceConfiguration).Assembly);

			return builder;
		}
	}
}
