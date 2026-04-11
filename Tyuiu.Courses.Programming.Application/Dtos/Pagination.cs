namespace Tyuiu.Courses.Programming.Application.Dtos
{
	public class Pagination
	{
		public Pages Pages { get; set; } = default!;
		public string? SearchTerm { get; set; }
	}
}
