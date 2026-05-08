using FluentValidation;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.CloneDiscipline
{
	public class CloneDisciplineCommandValidator : AbstractValidator<CloneDisciplineCommand>
	{
		public CloneDisciplineCommandValidator()
		{
			RuleFor(x => x.Id)
				.GreaterThan(0).WithMessage("Id дисциплины должен быть больше 0");

			RuleFor(x => x.NewDisciplineName)
				.MaximumLength(255).WithMessage("Название не должно превышать 255 символов")
				.When(x => !string.IsNullOrEmpty(x.NewDisciplineName));
		}
	}
}
