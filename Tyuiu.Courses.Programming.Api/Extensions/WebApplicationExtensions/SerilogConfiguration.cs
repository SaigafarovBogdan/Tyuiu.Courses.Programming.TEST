using Serilog;

namespace Tyuiu.Courses.Programming.Api.Extensions.WebApplicationExtensions
{
	public static class SerilogConfiguration
	{
		public static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder builder)
		{
			builder.Host.UseSerilog((context, config) =>
			{
				config
					.Enrich.FromLogContext()
					.Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
					.WriteTo.Console()
					.WriteTo.OpenTelemetry(options =>
					{
						options.Endpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]
							  ?? "http://localhost:4317";
						options.Protocol = Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc;
						options.ResourceAttributes = new Dictionary<string, object>
						{
							["service.name"] = context.HostingEnvironment.ApplicationName
						};
					});
			});

			return builder;
		}
	}
}
