using Tyuiu.Courses.Programming.Application.Dtos;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.GetDisciplinesPaginated
{
	public class DisciplineSearchModel
	{
		public List<DisciplineSearchItemDto> Disciplines { get; set; } = [];
		public Pagination Pagination { get; set; } = new();

	}
}
