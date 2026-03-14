using Tyuiu.Courses.Programming.Application.Abstractions;
using Tyuiu.Courses.Programming.Core.Shared;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.GetDisciplinesPaginated
{
	public record class GetDisciplinesPaginatedQuery(
		int Page = 1,
		int PageSize = 10,
		string? SearchTerm = null) : IQuery<Result<DisciplineSearchModel>>;
}
