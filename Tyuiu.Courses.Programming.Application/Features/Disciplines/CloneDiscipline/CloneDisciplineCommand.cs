using Tyuiu.Courses.Programming.Application.Abstractions;

namespace Tyuiu.Courses.Programming.Application.Features.Disciplines.CloneDiscipline
{
	public record class CloneDisciplineCommand(
		int Id, 
		string? NewDisciplineName) : ICommand;
}
