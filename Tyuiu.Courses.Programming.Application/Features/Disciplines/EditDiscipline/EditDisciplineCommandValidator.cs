using FluentValidation;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.EditDiscipline
{
	public class EditDisciplineCommandValidator : AbstractValidator<EditDisciplineCommand>
	{
		public EditDisciplineCommandValidator()
		{
			RuleFor(x => x.Id)
				.GreaterThan(0).WithMessage("Id дисциплины должен быть больше 0");

			RuleFor(x => x.DisciplineName)
				.NotEmpty().WithMessage("Название дисциплины обязательно")
				.MaximumLength(255).WithMessage("Название не должно превышать 255 символов");
		}
	}
}
