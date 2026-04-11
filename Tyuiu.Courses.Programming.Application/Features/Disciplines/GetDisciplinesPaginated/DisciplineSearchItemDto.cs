namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.GetDisciplinesPaginated
{
	public class DisciplineSearchItemDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string AuthorName { get; set; } = string.Empty;
		public bool IsCurrentUserAuthor { get; set; }
		public bool HasCourses { get; set; }
	}
}
