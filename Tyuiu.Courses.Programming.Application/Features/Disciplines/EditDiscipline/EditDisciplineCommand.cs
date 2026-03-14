using Tyuiu.Courses.Programming.Application.Abstractions;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.EditDiscipline
{
	public record class EditDisciplineCommand(
		int Id, 
		string DisciplineName): ICommand;
}
