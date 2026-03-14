using Tyuiu.Courses.Programming.Application.Dtos;
using Tyuiu.Courses.Programming.Infrastructure.Persistence.Entitites;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.GetDisciplinesPaginated
{
	public class DisciplineSearchModel
	{
		public Dictionary<DisciplineEntity, bool> Disciplines { get; set; }
		public Pagination Pagination;
	}
}
