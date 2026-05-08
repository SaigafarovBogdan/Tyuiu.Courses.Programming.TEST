using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence
{
	public class DesignTimeDatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
	{
		private const string ConnectionStringEnvVar = "ConnectionStrings__aspnetappdb";

		public DatabaseContext CreateDbContext(string[] args)
		{
			var connectionString = Environment.GetEnvironmentVariable(ConnectionStringEnvVar);

			Console.WriteLine($"\nConnection string value: {connectionString ?? "NULL"}");

			if (string.IsNullOrWhiteSpace(connectionString))
			{
				Console.WriteLine($"ERROR: Connection string not found!");
				throw new InvalidOperationException(
					$"Connection string not found. Please set the '{ConnectionStringEnvVar}' environment variable");
			}

			try
			{
				var builder = new NpgsqlConnectionStringBuilder(connectionString);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to parse connection string: {ex.Message}");
			}

			var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
			optionsBuilder.UseNpgsql(connectionString, npgsqlOptions =>
			{
				npgsqlOptions.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName);
			});

			return new DatabaseContext(optionsBuilder.Options);
		}
	}
}
