namespace Tyuiu.Courses.Programming.Application.Dtos
{
	public class Pagination
	{
		public string AspController;
		public string AspAction;

		public Pages Pages { get; set; } = default!;
		public string? SearchTerm { get; set; }
	}
}
