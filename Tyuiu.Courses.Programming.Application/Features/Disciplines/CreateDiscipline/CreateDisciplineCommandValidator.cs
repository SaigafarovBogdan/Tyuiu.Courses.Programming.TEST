using FluentValidation;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.CreateDiscipline
{
	public class CreateDisciplineCommandValidator : AbstractValidator<CreateDisciplineCommand>
	{
		public CreateDisciplineCommandValidator()
		{
			RuleFor(x => x.DisciplineName)
				.NotEmpty().WithMessage("Название дисциплины обязательно")
				.MaximumLength(255).WithMessage("Название не должно превышать 255 символов");
		}
	}
}
