using Tyuiu.Courses.Programming.Application.Abstractions;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.DeleteDiscipline
{
	public record class DeleteDisciplineCommand(int Id) : ICommand;
}
