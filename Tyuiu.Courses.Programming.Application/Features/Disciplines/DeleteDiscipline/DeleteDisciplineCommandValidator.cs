using FluentValidation;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.DeleteDiscipline
{
	internal class DeleteDisciplineCommandValidator : AbstractValidator<DeleteDisciplineCommand>
	{
		public DeleteDisciplineCommandValidator()
		{
			RuleFor(x => x.Id)
				.GreaterThan(0).WithMessage("Id дисциплины должен быть больше 0");
		}
	}
}
