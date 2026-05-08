using FluentValidation;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.GetDisciplinesPaginated
{
	public class GetDisciplinesPaginatedQueryValidator : AbstractValidator<GetDisciplinesPaginatedQuery>
	{
		public GetDisciplinesPaginatedQueryValidator()
		{
			RuleFor(x => x.Page)
				.GreaterThanOrEqualTo(1).WithMessage("Страница не может быть меньше 1");

			RuleFor(x => x.PageSize)
				.GreaterThanOrEqualTo(1).WithMessage("Размер страницы не может быть меньше 1");
		}
	}
}
