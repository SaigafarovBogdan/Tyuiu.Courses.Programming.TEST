namespace Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites.Extensions
{
	public static class DisciplineEntityExtensions
	{
		public static bool HasCourses(this DisciplineEntity discipline)
		{
			return discipline?.Courses?.Any() ?? false;
		}
	}
}
