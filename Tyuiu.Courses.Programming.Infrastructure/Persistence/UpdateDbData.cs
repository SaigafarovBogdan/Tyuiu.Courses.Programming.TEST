using Microsoft.Extensions.DependencyInjection;

namespace Tyuiu.Courses.Programming.Infrastructure.Persistence
{
	// Controller for update db data after change models and data
	// Remove update methods after deployment
	public class UpdateDbData
	{
		private DatabaseContext _context { get; }

		public UpdateDbData(IServiceProvider serviceProvider)
		{
			_context = serviceProvider.GetRequiredService<DatabaseContext>();
		}

		public async Task InitializeAsync()
		{
		}
	}
}
